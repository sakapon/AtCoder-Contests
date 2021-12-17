using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, x) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);

		for (int i = 1; i <= n; i++)
		{
			var d = Dijkstra_Tree(n + 1, v => map[v], i);
			if (d.Contains(x)) return true;
		}

		return false;
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

	public static long[] Dijkstra_Tree(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var c = costs[v];
			if (v == ev) break;

			foreach (var e in nexts(v))
			{
				var (nv, nc) = (e[1], c + e[2]);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
