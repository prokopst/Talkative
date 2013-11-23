using System;
using System.Collections.Generic;
using Talkative.Core;

namespace Talkative
{
	public class SubpartNotFound : Exception
	{
		public SubpartNotFound (string what) : base(what)
		{}
	}
	/// <summary>
	/// Instances of Reaction represents reaction of either NPC or user.
	/// </summary>
	public class Reaction
	{
		protected Node _node;
		protected Conversation _conversation;
		protected IGameInterface _gameInterface;
		protected IEnumerable<string> _langs;

		internal Reaction(Node node, Conversation conversation, IGameInterface gameInterface, IEnumerable<string> langs)
		{
			_node = node;
			_conversation = conversation;
			_gameInterface = gameInterface;
			_langs = langs;
		}

		protected Reaction ReactionFromNode (Node node)
		{
			return new Reaction(node, _conversation, _gameInterface, _langs);
		}

		/// <summary>
		/// Tries to get possible reactions to this one.
		/// </summary>
		/// <returns>
		/// True if there was at least one.
		/// </returns>
		/// <param name='reactions'>
		/// If set to <c>true</c> all possible reactions.
		/// </param>
		public bool TryGetPossibleReactions(out IList<Reaction> reactions)
		{
			reactions = new List<Reaction> ();
			Group group = null;
			if (_conversation.TryGetGroup(_node, out group) == false)
				return false;

			foreach (var node in group)
			{
				if (node.TestConditions (_gameInterface)) {
					Reaction reaction = ReactionFromNode (node);
					reactions.Add (reaction);
				}
			}
			return reactions.Count > 0;
		}

		/// <summary>
		/// Tries to get the best reaction. The best reaction is the last one which fullfil all conditions
		/// in its group.
		/// </summary>
		/// <returns>
		/// The get best reaction.
		/// </returns>
		/// <param name='reaction'>
		/// If set to <c>true</c> reaction.
		/// </param>
		public bool TryGetBestReaction(out Reaction reaction)
		{
			reaction = null;
			Group group = null;
			if (_conversation.TryGetGroup(_node, out group) == false)
				return false;
			Node latest = null;
			foreach (var node in group)
				if (node.TestConditions (_gameInterface))
					latest = node;
			if (latest != null)
			{
				reaction = ReactionFromNode(latest);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Trigger this instance.
		/// </summary>
		public void Trigger()
		{
			_node.ProcessActions(_gameInterface);
		}

		/// <summary>
		/// Gets the text. It uses other languages as fallback.
		/// </summary>
		/// <returns>
		/// The text.
		/// </returns>
		public string GetText()
		{
			Speech speech;
			foreach (var lang in _langs)
				if (_node.TryGetSpeechByLang(lang, out speech))
					if (speech.Text != null)
						return speech.Text;
			return null;
		}

		/// <summary>
		/// Gets the audio.
		/// </summary>
		/// <returns>
		/// The audio.
		/// </returns>
		public string GetAudio()
		{
			Speech speech;
			foreach (var lang in _langs)
				if (_node.TryGetSpeechByLang(lang, out speech))
					if (speech.Audio != null)
						return speech.Audio;
			return null;
		}
	}
}
