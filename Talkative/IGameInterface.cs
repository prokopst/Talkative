using System;

namespace Talkative
{
	public interface IGameInterface
	{
		string GetValuesAsString(string key);
		void SetValueAsString(string key, string value);
		double GetValueAsNumber(string key);
		void SetValueAsNumber(string key, double value);
		bool GetValueAsBool(string key);
		void SetValueAsBool(string key, bool value);
		void CallActionCallback(string name, params string[] variables);
		bool CallConditionCallback(string name, params string[] variables);
	}
}

