using System;
using System.Collections.Generic;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2();
		var h = Read();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMapList(n + 1, es, false);
		return Dfs(x, -1);

		int Dfs(int v, int pv)
		{
			var r = 0;
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				var nr = Dfs(nv, v);
				r += nr;
				if (nr > 0 || h[nv - 1] > 0) r += 2;
			}
			return r;
		}
	}

	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
