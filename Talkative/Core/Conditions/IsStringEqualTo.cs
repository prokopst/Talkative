using System;
using System.Xml.Serialization;
using Talkative;

namespace Talkative.Core.Conditions
{
	public class IsStringEqualTo : ICondition
	{
		[XmlAttribute("key")]
		public string Key = string.Empty;

		public string Value = string.Empty;

		public bool Test(IGameInterface storage)
		{
			string storageValue = storage.GetValuesAsString(Key);
			return storageValue == Value;
		}
	}
}

