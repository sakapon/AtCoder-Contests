using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<(int to, bool even)>());
		foreach (var (u, v, w) in es)
		{
			map[u].Add((v, w % 2 == 0));
			map[v].Add((u, w % 2 == 0));
		}

		var dp = new int[n + 1];
		DFS(1, -1);
		return string.Join("\n", dp[1..]);

		void DFS(int v, int pv)
		{
			foreach (var (nv, b) in map[v])
			{
				if (nv == pv) continue;
				dp[nv] = b ? dp[v] : 1 - dp[v];
				DFS(nv, v);
			}
		}
	}
}
