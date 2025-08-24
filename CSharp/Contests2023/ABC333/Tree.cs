using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Trees.Trees2401
{
	[System.Diagnostics.DebuggerDisplay(@"Root = {Root.Id}, Count = {Count}")]
	public class Tree
	{
		public static int[][] ToMap(int n, int[][] es, bool twoway) => ToMap(n, Array.ConvertAll(es, e => (e[0], e[1])), twoway);
		public static int[][] ToMap(int n, (int u, int v)[] es, bool twoway)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				if (twoway) map[v].Add(u);
			}
			return Array.ConvertAll(map, l => l.ToArray());
		}

		[System.Diagnostics.DebuggerDisplay(@"Id = {Id}, Count = {Count}")]
		public class Node
		{
			public int Id, Depth = -1, Count = 1;
			public Node Parent;
			public List<Node> Children = new List<Node>();
			public bool IsLeaf => Children.Count == 0;
			public bool IsRootLeaf => Parent == null && Children.Count == 1;
		}

		readonly Node[] nodes;
		public Node[] Nodes => nodes;
		public Node this[int id] => nodes[id];
		public int Count => nodes.Length;
		public Node Root { get; }

		public Tree(int[][] map, int root)
		{
			var n = map.Length;
			nodes = new Node[n];
			for (int id = 0; id < n; ++id) nodes[id] = new Node { Id = id };
			Root = nodes[root];
			Root.Depth = 0;
			DFS(Root, null);

			void DFS(Node v, Node pv)
			{
				foreach (var nid in map[v.Id])
				{
					var nv = nodes[nid];
					if (nv == pv) continue;
					v.Children.Add(nv);
					nv.Parent = v;
					nv.Depth = v.Depth + 1;
					DFS(nv, v);
					v.Count += nv.Count;
				}
			}
		}
	}
}
