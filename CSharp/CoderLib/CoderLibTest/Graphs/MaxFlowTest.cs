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
	}
}
