using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	// 有効な vertex id: [0, n)
	// edge id を利用しません。
	public class UGraph
	{
		public int VertexesCount { get; }
		List<int>[] map;
		public List<int>[] Map => map;

		public UGraph(int n, List<int>[] map)
		{
			VertexesCount = n;
			this.map = map;
		}
		public UGraph(int n) : this(n, Array.ConvertAll(new bool[n], _ => new List<int>())) { }
		public UGraph(int n, int[][] edges, bool directed) : this(n)
		{
			AddEdges(edges, directed);
		}

		public void AddEdge(int from, int to, bool directed)
		{
			map[from].Add(to);
			if (!directed) map[to].Add(from);
		}
		public void AddEdge(int[] e, bool directed) => AddEdge(e[0], e[1], directed);

		public void AddEdges(int[][] edges, bool directed)
		{
			foreach (var e in edges) AddEdge(e[0], e[1], directed);
		}

		public int[][] GetMapAsArray() => Array.ConvertAll(map, l => l.ToArray());
	}

	public class UTree
	{
		[System.Diagnostics.DebuggerDisplay(@"\{{Id}\}")]
		public class Node
		{
			public int Id;
			public int Depth = -1;
			public List<int> Orders = new List<int>();
			public Node Parent;
		}

		public UGraph Graph { get; }
		public int VertexesCount => Graph.VertexesCount;
		public List<int>[] Map => Graph.Map;

		public Node Root { get; }
		public Node[] Nodes { get; }
		// この Euler tour では方向を記録しません。
		// order -> vertex
		public Node[] TourNodes { get; }

		public UTree(UGraph graph, int root)
		{
			Graph = graph;
			var n = VertexesCount;
			Nodes = new Node[n];
			for (int i = 0; i < n; ++i) Nodes[i] = new Node { Id = i };
			TourNodes = new Node[2 * n - 1];

			Root = Nodes[root];
			Root.Depth = 0;
			Dfs(Root, new Node { Id = -1 });
		}

		int order = -1;
		void Dfs(Node cn, Node pn)
		{
			cn.Orders.Add(++order);
			TourNodes[order] = cn;

			foreach (var nv in Map[cn.Id])
			{
				if (nv == pn.Id) continue;
				var nn = Nodes[nv];
				nn.Depth = cn.Depth + 1;
				nn.Parent = cn;
				Dfs(nn, cn);

				cn.Orders.Add(++order);
				TourNodes[order] = cn;
			}
		}
	}
}
