using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using Talkative.Core;

namespace Talkative
{
	/// <summary>
	/// Conversation factory.
	/// </summary>
	public class ConversationFactory
	{
		protected IGameInterface _gameInterface;
		protected IEnumerable<string> _langs;

		public ConversationFactory(IGameInterface game, IEnumerable<string> langs)
		{
			_gameInterface = game;
			_langs = langs;
		}

		/// <summary>
		/// Loads the convesation from given path.
		/// </summary>
		/// <returns>
		/// First NPC's reaction in conversation.
		/// </returns>
		/// <param name='path'>
		/// Path.
		/// </param>
		public Reaction LoadConversationFromPath(string path)
		{
			StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open));
			return LoadConversation(reader);
		}

		public Reaction LoadConversation(TextReader reader, string startNode = null)
		{
			if (string.IsNullOrEmpty(startNode))
				startNode = "start";
			XmlSerializer serializer = new XmlSerializer (typeof(Conversation));
			Conversation conversation = serializer.Deserialize (reader) as Conversation;
			Node node = null;
			if (conversation.TryGetNode (startNode, _gameInterface, out node))
			{
				Reaction reaction = new Reaction(node, conversation, _gameInterface, _langs);
				return reaction;
			}
			return null;
		}

		public Reaction LoadConversation(string text, string startNode = null)
		{
			return LoadConversation(new StringReader(text));
		}
	}
}
