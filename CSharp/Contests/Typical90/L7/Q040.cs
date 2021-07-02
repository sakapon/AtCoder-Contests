using System;
using System.Collections.Generic;
using System.Linq;

class Q040
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, w) = Read2();
		var a = Read();
		var c = Array.ConvertAll(new bool[n], _ => Read()[1..]);

		long amax = a.Max();
		var sv = 0;
		var ev = n + 1;
		var rn = Enumerable.Range(0, n).ToArray();

		var es = new List<long[]>();
		es.AddRange(rn.Select(i => new[] { sv, i + 1, amax + w - a[i] }));
		es.AddRange(rn.Select(i => new[] { i + 1, ev, amax }));

		for (int i = 0; i < n; i++)
			foreach (var nv in c[i])
				es.Add(new long[] { i + 1, nv, 1L << 50 });

		var mf = new MaxFlow(ev + 1);
		mf.AddEdges(es.ToArray());
		var r = mf.Dinic(sv, ev);
		return n * amax - r;
	}
}

public class MaxFlow
{
	public class Edge
	{
		public int From, To, RevIndex;
		public long Capacity;
		public Edge(int from, int to, long capacity, int revIndex) { From = from; To = to; Capacity = capacity; RevIndex = revIndex; }
	}

	List<Edge>[] map;
	public Edge[][] Map;
	int[] depth;
	int[] cursor;
	Queue<int> q = new Queue<int>();

	public MaxFlow(int n)
	{
		map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
		depth = new int[n];
		cursor = new int[n];
	}

	public void AddEdge(int from, int to, long capacity)
	{
		map[from].Add(new Edge(from, to, capacity, map[to].Count));
		map[to].Add(new Edge(to, from, 0, map[from].Count - 1));
	}

	// { from, to, capacity }
	public void AddEdges(int[][] des)
	{
		foreach (var e in des) AddEdge(e[0], e[1], e[2]);
	}
	public void AddEdges(long[][] des)
	{
		foreach (var e in des) AddEdge((int)e[0], (int)e[1], e[2]);
	}

	void Bfs(int sv)
	{
		// Array.Fill が存在しない環境に対応するため、未到達点の深さを 0 とします。
		Array.Clear(depth, 0, depth.Length);
		depth[sv] = 1;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var e in Map[v])
			{
				if (e.Capacity == 0) continue;
				if (depth[e.To] > 0) continue;
				depth[e.To] = depth[v] + 1;
				q.Enqueue(e.To);
			}
		}
	}

	long Dfs(int v, int ev, long fMin)
	{
		if (v == ev) return fMin;

		for (; cursor[v] < Map[v].Length; ++cursor[v])
		{
			var e = Map[v][cursor[v]];
			if (e.Capacity == 0) continue;
			if (depth[v] >= depth[e.To]) continue;

			var delta = Dfs(e.To, ev, Math.Min(fMin, e.Capacity));
			if (delta > 0)
			{
				e.Capacity -= delta;
				Map[e.To][e.RevIndex].Capacity += delta;
				return delta;
			}
		}
		return 0;
	}

	public long Dinic(int sv, int ev)
	{
		Map = Array.ConvertAll(map, l => l.ToArray());

		long M = 0, t;
		while (true)
		{
			Bfs(sv);
			if (depth[ev] == 0) break;
			Array.Clear(cursor, 0, cursor.Length);
			while ((t = Dfs(sv, ev, long.MaxValue)) > 0) M += t;
		}
		return M;
	}

	// 0 <= v1 < n1, 0 <= v2 < n2
	public static int[][] BipartiteMatching(int n1, int n2, int[][] des)
	{
		int sv = n1 + n2, ev = sv + 1;
		var mf = new MaxFlow(ev + 1);

		for (int i = 0; i < n1; ++i)
			mf.AddEdge(sv, i, 1);
		for (int j = 0; j < n2; ++j)
			mf.AddEdge(n1 + j, ev, 1);
		foreach (var e in des)
			mf.AddEdge(e[0], n1 + e[1], 1);

		mf.Dinic(sv, ev);
		var map = mf.Map;

		var r = new List<int[]>();
		foreach (var se in map[sv])
		{
			if (se.Capacity > 0) continue;

			foreach (var e in map[se.To])
			{
				if (e.Capacity == 0)
				{
					r.Add(new[] { se.To, e.To - n1 });
					break;
				}
			}
		}
		return r.ToArray();
	}
}
