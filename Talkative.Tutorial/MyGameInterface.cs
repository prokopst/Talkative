using System;
using System.Collections.Generic;
using Talkative;

namespace Talkative.Tutorial
{
	public class MyGameInterface : Dictionary<string, string>, IGameInterface
	{
		public double GetValueAsNumber(string key)
		{
			double result = 0d;
			string value = string.Empty;
			if (TryGetValue(key, out value))
				if (double.TryParse(value, out result))
					return result;
			else
				this[key] = "0";
			return 0d;
		}

		public void SetValueAsNumber(string key, double value)
		{
			this[key] = value.ToString();
		}

		public bool GetValueAsBool(string key)
		{
			bool result = false;
			string value = string.Empty;
			if (TryGetValue(key, out value))
				if (bool.TryParse(value, out result))
					return result;
			else
				this[key] = bool.FalseString;
			return false;
		}

		public void SetValueAsBool(string key, bool value)
		{
			this[key] = value.ToString();
		}

		public void CallActionCallback(string name, params string[] variables) {throw new NotImplementedException();}
		public bool CallConditionCallback(string name, params string[] variables) {throw new NotImplementedException();}
		public string GetValuesAsString(string key) {throw new NotImplementedException();}
		public void SetValueAsString(string key, string value) {throw new NotImplementedException();}
	}
}

