using System;
using System.Xml.Serialization;

namespace Talkative.Core.Actions
{
	public class SetString : IAction
	{
		[XmlAttribute("key")]
		public string Key;

		[XmlAttribute("value")]
		public string Value;

		public void Process(IGameInterface game)
		{
			game.SetValueAsString(Key, Value);
		}
	}
}
