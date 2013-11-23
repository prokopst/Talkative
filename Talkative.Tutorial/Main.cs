using System;
// Added:
using System.Xml.Serialization;
// To open file:
using System.IO;
// For IList<Type>
using System.Collections.Generic;
// Our library:
using Talkative;
using System.Reflection;
using Talkative.Core.Actions;

namespace Talkative.Tutorial
{
	/// <summary>
	/// This is basically new C# Command-line project created in Mono.
	/// </summary>
	class MainClass
	{
		public static Reaction GiveChoices(IList<Reaction> reactions)
		{
			int count = reactions.Count;

			int i;
			for (i = 0; i < count; ++i)
			{
				// Instead of 0..n we use 1..n+1
				System.Console.Write(i + 1);
				System.Console.Write(") ");
				System.Console.WriteLine(reactions[i].GetText());
			}

			i = 0;
			string line = "";
			while (int.TryParse(line, out i) == false)
			{
				System.Console.WriteLine("Pick a choice:");
				line = System.Console.ReadLine();
			}
			return reactions[i-1];
		}



		public static void Main (string[] args)
		{
			Assembly assembly = typeof(IAction).Assembly;
			Type type = assembly.GetType("Talkative.Core.Actions.SetTrue");
			object obj = Activator.CreateInstance(type);
			// Create interface to game logic
			IGameInterface gameInterface = new MyGameInterface();

			// List of languages - the first one is important, the others are just fallback
			// In this case we expect only and only en-US
			string[] langs = {"en-US"};
			ConversationFactory factory = new ConversationFactory(gameInterface, langs);
			// Load conversation. The result is first NPC's reaction.
			Reaction npcReaction = factory.LoadConversationFromPath("conversation.xml");
			// Variable for user's reaction
			Reaction userReaction = null;
			// List of possible reactions that will be read by user
			IList<Reaction> reactions = null;

			// We will end this loop within body
			while (true)
			{
				// Print NPC's reaction
				System.Console.WriteLine(npcReaction.GetText());
				// Trigger it (call all actions)
				npcReaction.Trigger();
				// Get all possible reactions
				if (npcReaction.TryGetPossibleReactions(out reactions) == false)
					break;
				// Provide choices to user and let him choose the right reaction
				userReaction = GiveChoices(reactions);
				// Trigger it (call all actions)
				userReaction.Trigger();
				// Try to find best NPC's reaction, if any
				if (userReaction.TryGetBestReaction(out npcReaction) == false)
					break;
			}
		}
	}
}
