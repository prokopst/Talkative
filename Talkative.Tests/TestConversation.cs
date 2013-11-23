using NUnit.Framework;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using Talkative.Core;


namespace Tests
{
	[TestFixture]
	public class TestDialogue
	{
		[Test]
		public void ShouldCreateEmptyDialogIfXmlEmpty()
		{
			string data = @"<?xml version=""1.0"" encoding=""utf-16"" ?> 
				<Conversation>
				</Conversation>";
			StringReader stringReader = new StringReader(data);

			XmlSerializer serializer = new XmlSerializer(typeof(Conversation));
			Conversation dialogue = serializer.Deserialize(stringReader) as Conversation;
			Assert.IsNotNull(dialogue);
			Assert.IsEmpty(dialogue.Groups);
		}

		[Test]
		public void ShouldCreateDialogIfXmHasGroupWithManyTexts()
		{
			string data = @"<?xml version=""1.0"" ?>
				<Conversation>
					<Groups>
						<Group id=""12"">
							<Node>
								<Talks>
									<Talk lang=""en-US"">Not enough gasoline!</Talk>
									<Talk lang=""cs-CS"">Nedostatek benzínu!</Talk>
									<Talk lang=""en-GB"">Not enough petrol!</Talk>
								</Talks>
							</Node>
						</Group>
					</Groups>
				</Conversation>";
			StringReader stringReader = new StringReader(data);

			XmlSerializer serializer = new XmlSerializer(typeof(Conversation));
			Conversation dialogue = (Conversation)serializer.Deserialize(stringReader);
			Assert.NotNull(dialogue);

			Assert.AreEqual(1, dialogue.Groups.Count);

			List<Node> group = dialogue.Groups["12"];
			Assert.AreEqual(group[0].Speeches.Count, 3);

			//Assert.AreEqual("Not enough gasoline!", group[0].Texts["en-US"]);
			//Assert.AreEqual("Nedostatek benzínu!", group[0].Texts["cs-CS"]);
			//Assert.AreEqual("Not enough petrol!", group[0].Texts["en-GB"]);
		}

		[Test]
		public void ShouldCreateConversationIfXmHasManyGroups()
		{
			string data = @"<?xml version=""1.0"" ?>
				<Conversation>
					<Groups>
						<Group id=""12""></Group>
						<Group id=""13""></Group>
						<Group id=""14""></Group>
					</Groups>
				</Conversation>";
			StringReader stringReader = new StringReader(data);

			XmlSerializer serializer = new XmlSerializer(typeof(Conversation));
			Conversation dialogue = (Conversation)serializer.Deserialize(stringReader);
			Assert.NotNull(dialogue);

			Assert.AreEqual(3, dialogue.Groups.Count);
		}

		[Test]
		public void ShouldCreateConversationIfXmHasGroupWithManyNodes()
		{
			string data = @"<?xml version=""1.0"" ?>
				<Conversation>
					<Groups>
						<Group id=""12"">
							<Node>
								<Talks>
									<Talk lang=""en-US"">Not enough gasoline!</Talk>
								</Talks>
							</Node>
							<Node>
								<Talks>
									<Talk lang=""en-US"">Bye bye man!</Talk>
								</Talks>
							</Node>
							<Node>
								<Talks>
									<Talk lang=""en-US"">Not funny!</Talk>
								</Talks>
							</Node>
						</Group>
					</Groups>
				</Conversation>";
			StringReader stringReader = new StringReader(data);

			XmlSerializer serializer = new XmlSerializer(typeof(Conversation));
			Conversation dialogue = (Conversation)serializer.Deserialize(stringReader);
			Assert.NotNull(dialogue);

			Assert.AreEqual(1, dialogue.Groups.Count);
			Assert.AreEqual(3, dialogue.Groups["12"].Count);
		}
	}
}
