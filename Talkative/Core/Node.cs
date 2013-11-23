using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Talkative.Core.Conditions;
using Talkative.Core.Actions;

namespace Talkative.Core
{
	public interface INode
	{
		bool TryGetSpeechByLang(string lang, out Speech result);

		bool TestConditions(IGameInterface gameInterface);

		void ProcessActions(IGameInterface gameInterface);
	}

	public class Node : INode
	{
		[XmlArray("Conditions")]
		[XmlArrayItem(typeof(IsNumberEqualTo)), XmlArrayItem(typeof(IsTrue)), XmlArrayItem(typeof(IsFalse)), XmlArrayItem(typeof(IsStringEqualTo))]
		public List<ICondition> Conditions;

		[XmlArray("Actions")]
		[XmlArrayItem(typeof(SetTrue))]
		public List<IAction> Actions;

		public Speeches Speeches;

		[XmlAttribute("targetGroup")]
		public string TargetGroup;

		[XmlAttribute("exit")]
		public bool Exit = false;

		public bool TryGetSpeechByLang(string lang, out Speech result)
		{
			return Speeches.TryGetValue(lang, out result);
		}

		public bool TestConditions(IGameInterface gameInterface)
		{
			foreach (var condition in Conditions)
			{
				if (condition.Test(gameInterface) == false)
					return false;
			}
			return true;
		}

		public void ProcessActions(IGameInterface gameInterface)
		{
			foreach (var action in Actions)
				action.Process(gameInterface);
		}
	}
}

