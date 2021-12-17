using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.Spp;

class K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read().Prepend(0).ToArray();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, false);
		var r = Dijkstra(n + 1, a, v => map[v], 1);
		return r[n];
	}

	public static int[][][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	public static long[] Dijkstra(int n, int[] a, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var dp = Array.ConvertAll(new bool[n], _ => 0L);
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);

		dp[sv] = a[sv];
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e[1], c + e[2]);
				if (costs[nv] < nc) continue;
				if (costs[nv] == nc)
				{
					dp[nv] = Math.Max(dp[nv], dp[v] + a[nv]);
					continue;
				}
				dp[nv] = dp[v] + a[nv];
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return dp;
	}
}
