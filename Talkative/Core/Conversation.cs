using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Talkative.Core
{
	public class GroupNotFound : System.Exception
	{
		public GroupNotFound (string message) : base(message)
		{
		}
	}

	public class Conversation
	{
		public Groups Groups;

		public bool TryGetGroup(string id, out Group result)
		{
			result = null;
			if (string.IsNullOrEmpty(id))
				return false;
			return Groups.TryGetValue(id, out result);
		}

		public bool TryGetGroup(Node node, out Group result)
		{
			return TryGetGroup(node.TargetGroup, out result);
		}

		public Group GetGroup (string id)
		{
			Group result = null;
			if (TryGetGroup(id, out result))
				return result;
			throw new GroupNotFound(id);
		}

		public Group GetGroup (Node node)
		{
			return GetGroup(node.TargetGroup);
		}

		public bool TryGetNode(string groupId, IGameInterface world, out Node result)
		{
			result = null;
			Group group = null;
			if (TryGetGroup(groupId, out group) == false)
				return false;
			// for each node in group call test and return the latest
			foreach (var node in group)
			{
				if (node.TestConditions(world) == true)
					result = node;
			}
			return result != null;
		}

		public bool TryGetNode(Node node, IGameInterface world, out Node result)
		{
			return TryGetNode(node.TargetGroup, world, out result);
		}

	}
}

