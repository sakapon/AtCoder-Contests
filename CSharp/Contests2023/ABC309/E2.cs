using System;
using System.Collections.Generic;

class E2
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
		for (int i = 2; i <= n; i++)
		{
			map[p[i - 2]].Add(i);
		}

		var ys = new int[n + 1];
		foreach (var (x, y) in xys)
		{
			Chmax(ref ys[x], y);
		}

		var r = 0;
		DFS(1, 0, -1);
		return r;

		void DFS(int v, int d, int yd)
		{
			if (ys[v] > 0) Chmax(ref yd, d + ys[v]);
			if (d <= yd) r++;

			foreach (var nv in map[v])
			{
				DFS(nv, d + 1, yd);
			}
		}
	}

	static int Chmax(ref int x, int v) => x < v ? x = v : x;
}
