using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	// パスの復元が必要となる場合は、Map プロパティを使用します。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/6/GRL_6_B
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_e
	// Test: https://atcoder.jp/contests/past202005-open/tasks/past202005_o
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
}
