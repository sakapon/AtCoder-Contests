using System;
using System.Collections.Generic;
using System.Linq;

class A202211
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, sv) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMapList(n, es, false);
		var r = Dijkstra(n, v => map[v].ToArray(), sv);
		return string.Join("\n", r.Select(x => x == long.MaxValue ? "INF" : $"{x}"));
	}

	public static List<int[]>[] ToMapList(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (twoWay) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	public static long[] Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		costs[sv] = 0;
		var q = new SortedSet<(long, int)> { (0, sv) };

		while (q.Count > 0)
		{
			var (c, v) = q.Min;
			q.Remove((c, v));
			if (v == ev) break;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e[1], c + e[2]);
				if (costs[nv] <= nc) continue;
				if (costs[nv] != long.MaxValue) q.Remove((costs[nv], nv));
				q.Add((nc, nv));
				costs[nv] = nc;
			}
		}
		return costs;
	}
}
