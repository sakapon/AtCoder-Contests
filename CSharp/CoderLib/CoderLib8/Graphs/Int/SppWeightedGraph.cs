using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

// Test: https://judge.yosupo.jp/problem/shortest_path
// Test: https://atcoder.jp/contests/abc191/tasks/abc191_e
// Test: https://atcoder.jp/contests/abc237/tasks/abc237_e
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

		static readonly Edge[] EmptyEdges = new Edge[0];

		public int VertexesCount { get; }
		// map[v] が null である可能性があります。
		List<Edge>[] map;

		public SppWeightedGraph(int n)
		{
			VertexesCount = n;
			map = new List<Edge>[n];
		}

		public Edge[][] GetMap() => Array.ConvertAll(map, l => l?.ToArray() ?? EmptyEdges);

		public void AddEdge(int from, int to, long cost, bool directed) => AddEdge(new Edge(from, to, cost), directed);
		public void AddEdge(Edge e, bool directed)
		{
			var l = map[e.From] ?? (map[e.From] = new List<Edge>());
			l.Add(e);

			if (directed) return;
			l = map[e.To] ?? (map[e.To] = new List<Edge>());
			l.Add(e.GetReverse());
		}

		public void AddEdges(IEnumerable<int[]> es, bool directed)
		{
			foreach (var e in es) AddEdge(new Edge(e), directed);
		}
		public void AddEdges(IEnumerable<long[]> es, bool directed)
		{
			foreach (var e in es) AddEdge(new Edge(e), directed);
		}
		public void AddEdges(IEnumerable<Edge> es, bool directed)
		{
			foreach (var e in es) AddEdge(e, directed);
		}

		public long[] Dijkstra(int sv, int ev = -1) => Dijkstra(VertexesCount, v => map[v]?.ToArray() ?? EmptyEdges, sv, ev);

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
