using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var r = TopologicalSort(n + 1, es);
		return r == null;
	}

	public static int[] TopologicalSort(int n, int[][] des)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		var indeg = new int[n];
		foreach (var e in des)
		{
			map[e[0]].Add(e);
			++indeg[e[1]];
		}

		var r = new List<int>();
		var q = new Queue<int>();
		var svs = Array.ConvertAll(indeg, x => x == 0);

		// 連結されたグループごとに探索します。
		for (int sv = 0; sv < n; ++sv)
		{
			if (!svs[sv]) continue;

			r.Add(sv);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					r.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}
		}

		if (r.Count < n) return null;
		return r.ToArray();
	}
}
