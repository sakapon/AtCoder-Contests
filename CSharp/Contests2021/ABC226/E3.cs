using System;
using System.Collections.Generic;

class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, false);
		var u = new bool[n + 1];
		var r = 1L;
		var vc = 0;
		var ec = 0;

		for (int v = 1; v <= n; v++)
		{
			if (u[v]) continue;

			vc = ec = 0;
			Dfs(v);

			if (vc * 2 != ec) return 0;
			r = r * 2 % M;
		}
		return r;

		void Dfs(int v)
		{
			if (u[v]) return;
			u[v] = true;
			vc++;

			foreach (var nv in map[v])
			{
				Dfs(nv);
				ec++;
			}
		}
	}

	const long M = 998244353;

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
