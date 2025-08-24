using System;
using System.Collections.Generic;

class EDP2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var p = Read();
		var xys = Array.ConvertAll(new bool[m], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int v = 2; v <= n; v++)
		{
			map[p[v - 2]].Add(v);
		}

		var ys = new int[n + 1];
		Array.Fill(ys, -1);
		foreach (var (x, y) in xys)
		{
			Chmax(ref ys[x], y);
		}

		var r = 0;
		DFS(1, -1);
		return r;

		void DFS(int v, int d)
		{
			if (d < ys[v]) d = ys[v];
			if (d >= 0) r++;

			foreach (var nv in map[v])
			{
				DFS(nv, d - 1);
			}
		}
	}

	static int Chmax(ref int x, int v) => x < v ? x = v : x;
}
