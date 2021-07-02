using System;
using System.Collections.Generic;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var mf = new MaxFlow(n);
		mf.AddEdges(es);
		Console.WriteLine(mf.Dinic(0, n - 1));
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
