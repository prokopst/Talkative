using System;

namespace Talkative.Core.Conditions
{
	public interface ICondition
	{
		bool Test(IGameInterface world);
	}
}

