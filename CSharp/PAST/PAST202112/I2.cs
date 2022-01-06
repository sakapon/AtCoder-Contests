using System;
using System.Collections.Generic;
using Bang.Graphs.Int.Spp;

class I2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2L();
		var es = Array.ConvertAll(new bool[m], _ => ReadL());

		var set = new HashSet<long> { 1, n };
		var spp = new SppWeightedGraph<long>();

		foreach (var e in es)
		{
			set.Add(e[0]);
			set.Add(e[1]);

			if (e[1] - e[0] > e[2])
				spp.AddEdge(e[0], e[1], e[2], false);
		}

		var xs = new long[set.Count];
		set.CopyTo(xs);
		Array.Sort(xs);

		for (int i = 1; i < xs.Length; i++)
		{
			spp.AddEdge(xs[i - 1], xs[i], xs[i] - xs[i - 1], false);
		}

		var r = spp.Dijkstra(1, n);
		return r[n];
	}
}

public class SppWeightedGraph<TVertex>
{
	public struct Edge
	{
		public TVertex From, To;
		public long Cost;
		public Edge(TVertex from, TVertex to, long cost) { From = from; To = to; Cost = cost; }
	}

	public Dictionary<TVertex, List<Edge>> Map = new Dictionary<TVertex, List<Edge>>();

	public void AddEdge(TVertex from, TVertex to, long cost, bool directed)
	{
		if (!Map.ContainsKey(from)) Map[from] = new List<Edge>();
		Map[from].Add(new Edge(from, to, cost));

		if (directed) return;
		if (!Map.ContainsKey(to)) Map[to] = new List<Edge>();
		Map[to].Add(new Edge(to, from, cost));
	}

	// 終点を指定しないときは、ev に null, -1 などを指定します。
	public Dictionary<TVertex, long> Dijkstra(TVertex sv, TVertex ev)
	{
		var eq = EqualityComparer<TVertex>.Default;
		var costs = new Dictionary<TVertex, long>();
		var q = PriorityQueue<TVertex>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (eq.Equals(v, ev)) break;
			if (costs[v] < c) continue;

			if (!Map.ContainsKey(v)) continue;
			foreach (var e in Map[v])
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
