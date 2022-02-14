using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

namespace CoderLib8.Graphs.Int
{
	public class SppWeightedGraph
	{
		public struct Edge
		{
			public static readonly Edge Invalid = new Edge(-1, -1, long.MinValue);

			public int From, To;
			public long Cost;
			public Edge(int from, int to, long cost) { From = from; To = to; Cost = cost; }
			public Edge(int[] e) : this(e[0], e[1], e[2]) { }
			public Edge(long[] e) : this((int)e[0], (int)e[1], e[2]) { }
			public Edge GetReverse() => new Edge(To, From, Cost);
		}

		public int VertexesCount { get; }
		public List<Edge>[] Map;

		public SppWeightedGraph(int n)
		{
			VertexesCount = n;
			Map = new List<Edge>[n];
		}

		public void AddEdge(int from, int to, long cost, bool directed)
		{
			if (Map[from] == null) Map[from] = new List<Edge>();
			Map[from].Add(new Edge(from, to, cost));

			if (directed) return;
			if (Map[to] == null) Map[to] = new List<Edge>();
			Map[to].Add(new Edge(to, from, cost));
		}

		static readonly Edge[] EmptyEdges = new Edge[0];
		public long[] Dijkstra(int sv, int ev = -1) => Dijkstra(VertexesCount, v => Map[v]?.ToArray() ?? EmptyEdges, sv, ev);

		// 終点を指定しないときは、-1 を指定します。
		public static long[] Dijkstra(int n, Func<int, Edge[]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
			costs[sv] = 0;
			q.Push(sv);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == ev) break;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e.To, c + e.Cost);
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Push(nv);
				}
			}
			return costs;
		}
	}
}
