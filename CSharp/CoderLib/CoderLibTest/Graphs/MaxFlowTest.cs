using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class MaxFlowTest
	{
		static long MaxFlow(int n, int sv, int ev, long[][] dag)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var e in dag)
			{
				map[e[0]].Add(new[] { e[1], e[2], map[e[1]].Count });
				map[e[1]].Add(new[] { e[0], 0, map[e[0]].Count - 1 });
			}

			long Bfs()
			{
				var from = new long[n + 1][];
				var u = new bool[n + 1];
				var minFlow = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
				var q = new Queue<long>();
				q.Enqueue(sv);

				while (q.TryDequeue(out var v))
				{
					if (v == ev) break;
					foreach (var e in map[v])
					{
						if (u[e[0]] || e[1] == 0) continue;
						from[e[0]] = e;
						u[e[0]] = true;
						minFlow[e[0]] = Math.Min(minFlow[v], e[1]);
						q.Enqueue(e[0]);
					}
				}

				if (from[ev] == null) return 0;
				long tv = ev;
				while (true)
				{
					from[tv][1] -= minFlow[ev];
					var rev = map[from[tv][0]][(int)from[tv][2]];
					rev[1] += minFlow[ev];
					if ((tv = rev[0]) == sv) break;
				}
				return minFlow[ev];
			}

			long M = 0, t;
			while ((t = Bfs()) > 0) M += t;
			return M;
		}

		static long MaxFlowByDfs(int n, int sv, int ev, long[][] dag)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var e in dag)
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

		static long MinCostFlow(int n, int sv, int ev, long[][] dag, long f)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
			foreach (var r in dag)
			{
				map[r[0]].Add(new[] { r[1], r[2], r[3], map[r[1]].Count });
				map[r[1]].Add(new[] { r[0], 0, -r[3], map[r[0]].Count - 1 });
			}

			long[][] from;
			long[] u, minFlow;
			void BellmanFord()
			{
				from = new long[n + 1][];
				u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
				minFlow = new long[n + 1];
				u[sv] = 0;
				minFlow[sv] = long.MaxValue;
				long t;
				var next = true;
				while (next)
				{
					next = false;
					for (int v = 0; v <= n; v++)
					{
						if (u[v] == long.MaxValue) continue;
						foreach (var r in map[v])
						{
							if (r[1] == 0 || (t = u[v] + r[2]) >= u[r[0]]) continue;
							from[r[0]] = r;
							u[r[0]] = t;
							minFlow[r[0]] = Math.Min(minFlow[v], r[1]);
							next = true;
						}
					}
				}
			}

			var cost = 0L;
			while (f > 0)
			{
				BellmanFord();
				if (from[ev] == null) break;

				var delta = Math.Min(minFlow[ev], f);
				f -= delta;
				cost += delta * u[ev];

				long v = ev;
				while (true)
				{
					from[v][1] -= delta;
					var rev = map[from[v][0]][(int)from[v][3]];
					rev[1] += delta;
					if ((v = rev[0]) == sv) break;
				}
			}
			return cost;
		}
	}
}
