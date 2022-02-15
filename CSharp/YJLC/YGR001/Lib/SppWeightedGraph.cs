using System;
using System.Collections.Generic;
using System.Linq;

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

	public void AddEdge(int from, int to, long cost, bool directed) => AddEdge(new Edge(from, to, cost), directed);
	public void AddEdge(Edge e, bool directed)
	{
		if (Map[e.From] == null) Map[e.From] = new List<Edge>();
		Map[e.From].Add(e);

		if (directed) return;
		if (Map[e.To] == null) Map[e.To] = new List<Edge>();
		Map[e.To].Add(e.GetReverse());
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

	static readonly Edge[] EmptyEdges = new Edge[0];
	public (long[] d, int[] from) Dijkstra(int sv, int ev = -1) => Dijkstra(VertexesCount, v => Map[v]?.ToArray() ?? EmptyEdges, sv, ev);

	// 終点を指定しないときは、-1 を指定します。
	public static (long[] d, int[] from) Dijkstra(int n, Func<int, Edge[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var from = Array.ConvertAll(costs, _ => -1);
		var q = PQ<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e.To, c + e.Cost);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				from[nv] = v;
				q.Push(nv);
			}
		}
		return (costs, from);
	}
}
