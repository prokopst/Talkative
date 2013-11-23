using System;
using Talkative;

namespace Talkative.Core.Actions
{
	public interface IAction
	{
		void Process(IGameInterface world);
	}
}

