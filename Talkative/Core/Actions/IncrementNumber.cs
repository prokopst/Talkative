using System;
using System.Xml.Serialization;

namespace Talkative.Core.Actions
{
	public class IncrementNumber : IAction
	{
		[XmlAttribute("key")]
		public string Key;

		[XmlAttribute("value")]
		public double Value;

		public void Process(IGameInterface game)
		{
			double number = game.GetValueAsNumber(Key);
			number += Value;
			game.SetValueAsNumber(Key, number);
		}
	}
}

