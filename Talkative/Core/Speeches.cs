using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace Talkative.Core
{
	[XmlRoot("Speeches")]
	public class Speeches : Dictionary<string, Speech>, IXmlSerializable
	{
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Speech));
			while (true)
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Speeches")
				{
					reader.Read();
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Speech") {
					string lang = reader.GetAttribute("lang");
					Speech speech = serializer.Deserialize(reader) as Speech;
					Add(lang, speech);
				}
				else {
					reader.Read();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			// <Speeches>
			writer.WriteStartElement("Speeches");
	        foreach (var pair in this)
	        {
				var key = pair.Key;
				var value = pair.Value;
				// <Speech
				writer.WriteStartElement("Speech");
				// lang="en-US">
				writer.WriteAttributeString("lang", key);
				// <Text>text</Text>
				writer.WriteElementString("Text", value.Text);
				// <Audio>text</Audio>
				writer.WriteElementString("Audio", value.Audio);
				// </Speech>
				writer.WriteEndElement();
	        }
			// </Speeches>
			writer.WriteEndElement();
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}
	}
}

