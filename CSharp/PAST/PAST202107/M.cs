﻿using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	const long max = 1L << 32;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = Read2();
		var a = Read();

		var sv = 2 * n;
		var ev = sv + 1;
		var mf = new MinCostFlow(ev + 1);

		for (int i = 0; i < n; ++i)
		{
			mf.AddEdge(sv, i, 1, c);
			mf.AddEdge(n + i, ev, 1, 0);
			mf.AddEdge(i, n + i, 1, -max);

			for (int j = i + 1; j < n; j++)
			{
				mf.AddEdge(n + i, j, 1, Math.Abs(a[i] - a[j]));
			}
		}

		return mf.GetMinCostForRange(sv, ev, n) + n * max;
	}
}

public class MinCostFlow
{
	public class Edge
	{
		public int From, To, RevIndex;
		public long Capacity, Cost;
		public Edge(int from, int to, long capacity, long cost, int revIndex) { From = from; To = to; Capacity = capacity; Cost = cost; RevIndex = revIndex; }
	}

	int n;
	List<Edge>[] map;
	public Edge[][] Map;

	public MinCostFlow(int n)
	{
		this.n = n;
		map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
	}

	public void AddEdge(int from, int to, long capacity, long cost)
	{
		map[from].Add(new Edge(from, to, capacity, cost, map[to].Count));
		map[to].Add(new Edge(to, from, 0, -cost, map[from].Count - 1));
	}

	// { from, to, capacity, cost }
	public void AddEdges(int[][] des)
	{
		foreach (var e in des) AddEdge(e[0], e[1], e[2], e[3]);
	}
	public void AddEdges(long[][] des)
	{
		foreach (var e in des) AddEdge((int)e[0], (int)e[1], e[2], e[3]);
	}

	long BellmanFord(int sv, int ev, ref long f)
	{
		var from = new Edge[n];
		var cost = Array.ConvertAll(from, _ => long.MaxValue);
		var minFlow = new long[n];
		cost[sv] = 0;
		minFlow[sv] = f;

		var next = true;
		while (next)
		{
			next = false;
			for (int v = 0; v < n; ++v)
			{
				if (cost[v] == long.MaxValue) continue;
				foreach (var e in Map[v])
				{
					if (e.Capacity == 0 || cost[e.To] <= cost[v] + e.Cost) continue;
					from[e.To] = e;
					cost[e.To] = cost[v] + e.Cost;
					minFlow[e.To] = Math.Min(minFlow[v], e.Capacity);
					next = true;
				}
			}
		}

		if (from[ev] == null) return long.MaxValue;
		for (var v = ev; v != sv; v = from[v].From)
		{
			var e = from[v];
			e.Capacity -= minFlow[ev];
			Map[e.To][e.RevIndex].Capacity += minFlow[ev];
		}
		f -= minFlow[ev];
		return minFlow[ev] * cost[ev];
	}

	// 到達不可能の場合、MaxValue を返します。
	public long GetMinCost(int sv, int ev, long f)
	{
		Map = Array.ConvertAll(map, l => l.ToArray());

		long r = 0, t;
		while (f > 0)
		{
			if ((t = BellmanFord(sv, ev, ref f)) == long.MaxValue) return t;
			r += t;
		}
		return r;
	}

	// 「fMax 以下」に対する最小費用を求めます。
	// 「fMax 以下」で考える場合、最も流量が大きいときに最小費用を達成するとは限りません。
	public long GetMinCostForRange(int sv, int ev, long fMax)
	{
		Map = Array.ConvertAll(map, l => l.ToArray());

		long m = long.MaxValue, mf = 0, f = fMax;
		long r = 0, t;
		while (f > 0)
		{
			if ((t = BellmanFord(sv, ev, ref f)) == long.MaxValue) return m;
			r += t;

			if (r < m)
			{
				m = r;
				mf = fMax - f;
			}
		}
		return m;

		// 流量も返す場合
		//return (m, mf);
	}
}
