using System;
using System.Collections.Generic;
using System.Linq;

class EDP
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

		var dp = new int[n + 1];
		Array.Fill(dp, -1);
		foreach (var (x, y) in xys)
		{
			Chmax(ref dp[x], y);
		}

		DFS(1);
		return dp.Count(x => x >= 0);

		void DFS(int v)
		{
			foreach (var nv in map[v])
			{
				Chmax(ref dp[nv], dp[v] - 1);
				DFS(nv);
			}
		}
	}

	static int Chmax(ref int x, int v) => x < v ? x = v : x;
}
