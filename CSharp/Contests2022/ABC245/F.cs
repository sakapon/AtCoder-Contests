using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, true);

		var u = new bool[n + 1];
		var end = new bool[n + 1];
		var path = new bool[n + 1];

		for (int v = 1; v <= n; v++)
		{
			Dfs(v);
		}
		return u.Count(b => b);

		bool Dfs(int v)
		{
			if (end[v]) return u[v];
			if (path[v]) return true;

			path[v] = true;

			foreach (var nv in map[v])
			{
				if (Dfs(nv))
				{
					path[v] = false;
					end[v] = true;
					return u[v] = true;
				}
			}

			path[v] = false;
			end[v] = true;
			return false;
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
