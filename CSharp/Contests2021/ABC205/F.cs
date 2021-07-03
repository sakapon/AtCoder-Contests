using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read4());

		var sv = 0;
		var ev = 401;
		var mf = new MaxFlow(ev + 1);

		var es = new List<long[]>();

		for (int i = 1; i <= h; i++)
			es.Add(new[] { sv, i, 1L });
		for (int i = 1; i <= w; i++)
			es.Add(new[] { 300 + i, ev, 1L });

		for (int i = 1; i <= n; i++)
		{
			var (a, b, c, d) = ps[i - 1];
			var ri = 100 + i;
			var ci = 200 + i;
			es.Add(new[] { ri, ci, 1L });

			for (int j = a; j <= c; j++)
				es.Add(new[] { j, ri, 1L });
			for (int j = b; j <= d; j++)
				es.Add(new[] { ci, 300 + j, 1L });
		}

		mf.AddEdges(es.ToArray());
		return mf.Dinic(sv, ev);
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
}
