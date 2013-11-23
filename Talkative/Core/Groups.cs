using System;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace Talkative.Core
{
	public class Groups : Dictionary<string, Group>, IXmlSerializable
	{
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Group));
			while (true)
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Groups")
				{
					reader.Read();
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Group") {
					Group group = serializer.Deserialize(reader) as Group;
					Add(group.Id, group);
				}
				else {
					reader.Read();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Groups");

			XmlSerializer serializer = new XmlSerializer(typeof(Group));
	        foreach (var pair in this)
	        {
				serializer.Serialize(writer, pair.Value);
	        }
			writer.WriteEndElement();
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}
	}
}

