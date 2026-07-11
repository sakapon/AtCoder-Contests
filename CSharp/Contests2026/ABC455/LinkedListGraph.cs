using System;
using System.Collections.Generic;

// 直線状のグラフを表します。
// 自己ループ、サイクルが発生する可能性があります。
namespace CoderLib8.Collections
{
	public class LinkedListGraph
	{
		public class Node
		{
			public int Id { get; }
			public Node Left { get; set; }
			public Node Right { get; set; }

			public Node(int id) => Id = id;
		}

		readonly int n;
		public int NodesCount => n;
		public Node[] Nodes { get; }

		public LinkedListGraph(int n)
		{
			this.n = n;
			Nodes = new Node[n];
			for (int v = 0; v < n; ++v)
				Nodes[v] = new Node(v);
		}

		public bool AddEdge(int l, int r)
		{
			if (Nodes[l].Right != null) return false;
			if (Nodes[r].Left != null) return false;
			Nodes[l].Right = Nodes[r];
			Nodes[r].Left = Nodes[l];
			return true;
		}
		public bool AddEdgeIgnore(int u, int v)
		{
			return AddEdge(u, v) || AddEdge(v, u);
		}

		public bool RemoveEdgeBefore(int v)
		{
			if (Nodes[v].Left == null) return false;
			Nodes[v].Left.Right = null;
			Nodes[v].Left = null;
			return true;
		}
		public bool RemoveEdgeAfter(int v)
		{
			if (Nodes[v].Right == null) return false;
			Nodes[v].Right.Left = null;
			Nodes[v].Right = null;
			return true;
		}

		public bool RemoveEdge(int l, int r)
		{
			if (Nodes[l].Right != Nodes[r]) return false;
			Nodes[l].Right = null;
			Nodes[r].Left = null;
			return true;
		}
		public bool RemoveEdgeIgnore(int u, int v)
		{
			return RemoveEdge(u, v) || RemoveEdge(v, u);
		}

		public IEnumerable<int> GetPath(int v)
		{
			var node0 = Nodes[v];
			var node = node0;
			while (node != null)
			{
				yield return node.Id;
				node = node.Right;
				if (node == node0) yield break;
			}
		}
		public IEnumerable<int> GetPathReverse(int v)
		{
			var node0 = Nodes[v];
			var node = node0;
			while (node != null)
			{
				yield return node.Id;
				node = node.Left;
				if (node == node0) yield break;
			}
		}
	}
}
