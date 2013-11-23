using System;
using System.Xml.Serialization;

namespace Talkative.Core.Actions
{
	public class SetTrue : IAction
	{
		[XmlAttribute("key")]
		public string Key;

		public void Process(IGameInterface game)
		{
			game.SetValueAsBool(Key, true);
		}
	}
}

