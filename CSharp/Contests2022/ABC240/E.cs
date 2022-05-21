using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);

		var result = new (int l, int r)[n];
		var t = 1;
		Dfs(1, -1);
		return string.Join("\n", result.Select(e => $"{e.l} {e.r}"));

		void Dfs(int v, int pv)
		{
			var (l, r) = (n + 1, 0);

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				Dfs(nv, v);

				l = Math.Min(l, result[nv - 1].l);
				r = Math.Max(r, result[nv - 1].r);
			}

			if (r == 0) (l, r) = (t, t++);
			result[v - 1] = (l, r);
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
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
