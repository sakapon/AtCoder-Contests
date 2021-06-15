using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class Q013A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, false);

		var r1 = Dijkstra(n + 1, v => map[v].ToArray(), 1);
		var r2 = Dijkstra(n + 1, v => map[v].ToArray(), n);

		return string.Join("\n", Enumerable.Range(1, n).Select(v => r1[v] + r2[v]));
	}

	static List<int[]>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	public static long[] Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PQ<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var (c, v) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e[1], c + e[2]);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return costs;
	}
}
