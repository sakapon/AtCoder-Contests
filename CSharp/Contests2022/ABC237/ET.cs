using System;
using System.Collections.Generic;
using System.Linq;

class ET
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var h = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var spp = new SppWeightedGraph<int>();

		foreach (var (u, v) in es)
		{
			var hu = h[u - 1];
			var hv = h[v - 1];

			spp.AddEdge(u, v, hu < hv ? hv - hu : 0, true);
			spp.AddEdge(v, u, hv < hu ? hu - hv : 0, true);
		}

		var d = spp.Dijkstra(1, -1);
		return Enumerable.Range(1, n).Max(v => h[0] - h[v - 1] - d[v]);
	}
}

public class SppWeightedGraph<TVertex>
{
	public struct Edge
	{
		public TVertex From, To;
		public long Cost;
		public Edge(TVertex from, TVertex to, long cost) { From = from; To = to; Cost = cost; }
		public Edge GetReverse() => new Edge(To, From, Cost);
	}

	public Dictionary<TVertex, List<Edge>> Map = new Dictionary<TVertex, List<Edge>>();

	public void AddEdge(TVertex from, TVertex to, long cost, bool directed) => AddEdge(new Edge(from, to, cost), directed);
	public void AddEdge(Edge e, bool directed)
	{
		if (!Map.ContainsKey(e.From)) Map[e.From] = new List<Edge>();
		Map[e.From].Add(e);

		if (directed) return;
		if (!Map.ContainsKey(e.To)) Map[e.To] = new List<Edge>();
		Map[e.To].Add(e.GetReverse());
	}

	public void AddEdges(IEnumerable<Edge> es, bool directed)
	{
		foreach (var e in es) AddEdge(e, directed);
	}

	static readonly Edge[] EmptyEdges = new Edge[0];
	public Dictionary<TVertex, long> Dijkstra(TVertex sv, TVertex ev) => Dijkstra(v => Map.ContainsKey(v) ? Map[v].ToArray() : EmptyEdges, sv, ev);

	// 終点を指定しないときは、ev に null, -1 などを指定します。
	public static Dictionary<TVertex, long> Dijkstra(Func<TVertex, Edge[]> nexts, TVertex sv, TVertex ev)
	{
		var comp = EqualityComparer<TVertex>.Default;
		var costs = new Dictionary<TVertex, long>();
		var q = PriorityQueue<TVertex>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (comp.Equals(v, ev)) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e.To, c + e.Cost);
				if (costs.ContainsKey(nv) && costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return costs;
	}
}
