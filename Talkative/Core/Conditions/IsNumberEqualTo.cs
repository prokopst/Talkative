using System;
using System.Xml.Serialization;
using Talkative;

namespace Talkative.Core.Conditions
{
	public class IsNumberEqualTo : ICondition
	{
		[XmlAttribute("key")]
		public string Key = string.Empty;

		[XmlAttribute("value")]
		public double Value = 0f;

		[XmlAttribute("delta")]
		public double Delta = 0f;

		public bool Test(IGameInterface storage)
		{
			double value = storage.GetValueAsNumber(Key);
			return Math.Abs(Value - value) < Delta;
		}
	}
}

