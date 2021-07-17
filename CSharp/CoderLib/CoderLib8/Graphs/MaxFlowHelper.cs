using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs
{
	// 値を更新するため、辺を構造体として定義すると煩雑になります。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/6/GRL_6_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/6/GRL_6_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/7/GRL_7_A
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_d
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_e
	static class MaxFlowHelper
	{
		// dg: { from, to, capacity }
		static long MaxFlow(int n, int sv, int ev, long[][] dg)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var e in dg)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2], map[e[1]].Count });
				map[e[1]].Add(new[] { e[1], e[0], 0, map[e[0]].Count - 1 });
			}

			long Bfs()
			{
				var from = new long[n + 1][];
				var minFlow = new long[n + 1];
				Array.Fill(minFlow, long.MaxValue);
				var q = new Queue<long>();
				q.Enqueue(sv);

				while (q.TryDequeue(out var v))
				{
					if (v == ev) break;
					foreach (var e in map[v])
					{
						if (from[e[1]] != null || e[2] == 0) continue;
						from[e[1]] = e;
						minFlow[e[1]] = Math.Min(minFlow[v], e[2]);
						q.Enqueue(e[1]);
					}
				}

				if (from[ev] == null) return 0;
				for (long v = ev; v != sv; v = from[v][0])
				{
					var e = from[v];
					e[2] -= minFlow[ev];
					map[e[1]][(int)e[3]][2] += minFlow[ev];
				}
				return minFlow[ev];
			}

			long M = 0, t;
			while ((t = Bfs()) > 0) M += t;
			return M;
		}

		// dg: { from, to, capacity }
		[Obsolete]
		static long MaxFlowByDfs(int n, int sv, int ev, long[][] dg)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var e in dg)
			{
				map[e[0]].Add(new[] { e[1], e[2], map[e[1]].Count });
				map[e[1]].Add(new[] { e[0], 0, map[e[0]].Count - 1 });
			}

			var u = new bool[n + 1];
			long Dfs(long v, long min)
			{
				if (v == ev) return min;
				u[v] = true;
				foreach (var e in map[v])
				{
					if (u[e[0]] || e[1] == 0) continue;
					var delta = Dfs(e[0], Math.Min(min, e[1]));
					if (delta > 0)
					{
						e[1] -= delta;
						map[e[0]][(int)e[2]][1] += delta;
						u[v] = false;
						return delta;
					}
				}
				u[v] = false;
				return 0;
			}

			long M = 0, t;
			while ((t = Dfs(sv, long.MaxValue)) > 0) M += t;
			return M;
		}

		// dg: { from, to, capacity, cost }
		// 到達不可能の場合、MaxValue を返します。
		static long MinCostFlow(int n, int sv, int ev, long[][] dg, long f)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var e in dg)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2], e[3], map[e[1]].Count });
				map[e[1]].Add(new[] { e[1], e[0], 0, -e[3], map[e[0]].Count - 1 });
			}

			long BellmanFord()
			{
				var from = new long[n + 1][];
				var cost = new long[n + 1];
				Array.Fill(cost, long.MaxValue);
				var minFlow = new long[n + 1];
				cost[sv] = 0;
				minFlow[sv] = f;

				var next = true;
				while (next)
				{
					next = false;
					for (int v = 0; v <= n; v++)
					{
						if (cost[v] == long.MaxValue) continue;
						foreach (var e in map[v])
						{
							if (e[2] == 0 || cost[e[1]] <= cost[v] + e[3]) continue;
							from[e[1]] = e;
							cost[e[1]] = cost[v] + e[3];
							minFlow[e[1]] = Math.Min(minFlow[v], e[2]);
							next = true;
						}
					}
				}

				if (from[ev] == null) return long.MaxValue;
				for (long v = ev; v != sv; v = from[v][0])
				{
					var e = from[v];
					e[2] -= minFlow[ev];
					map[e[1]][(int)e[4]][2] += minFlow[ev];
				}
				f -= minFlow[ev];
				return minFlow[ev] * cost[ev];
			}

			long r = 0, t;
			while (f > 0)
			{
				if ((t = BellmanFord()) == long.MaxValue) return t;
				r += t;
			}
			return r;
		}
	}
}
