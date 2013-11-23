using System;
using System.Xml.Serialization;

namespace Talkative.Core.Conditions
{
	public class IsTrue : ICondition
	{
		[XmlAttribute("key")]
		public string Key;

		public bool Test(IGameInterface game)
		{
			return game.GetValueAsBool(Key) == true;
		}
	}
}

