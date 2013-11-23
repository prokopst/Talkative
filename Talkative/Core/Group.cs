using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Talkative.Core
{
	public class Group : List<Node>, IXmlSerializable
	{
		public string Id;

		public void ReadXml(XmlReader reader)
		{
			Id = reader.GetAttribute("id");
			XmlSerializer serializer = new XmlSerializer(typeof(Node));
			while (true)
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Group")
				{
					reader.Read();
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Node") {
					Node node = serializer.Deserialize(reader) as Node;
					Add(node);
				}
				else {
					reader.Read();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Group");

			writer.WriteAttributeString("id", Id);
			XmlSerializer serializer = new XmlSerializer(typeof(Node));
	        foreach (var node in this)
	        {
				serializer.Serialize(writer, node);
	        }
			writer.WriteEndElement();
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}
	}
}

