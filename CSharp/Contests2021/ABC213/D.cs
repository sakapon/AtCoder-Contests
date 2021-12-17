using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);
		for (int v = 1; v <= n; v++)
		{
			Array.Sort(map[v]);
		}

		var sv = 1;
		var path = new List<int>();
		var u = new bool[n + 1];

		Dfs(sv);
		return string.Join(" ", path);

		void Dfs(int v)
		{
			u[v] = true;
			path.Add(v);

			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				Dfs(nv);
				path.Add(v);
			}
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
