using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		// no edge
		var map = new bool[n + 1, n + 1];
		foreach (var (u, v) in es)
		{
			map[u, v] = true;
			map[v, u] = true;
		}

		var dp = NewArray2(k + 1, n + 1, 0L);
		dp[0][1] = 1;

		for (int i = 1; i <= k; i++)
		{
			var sum = dp[i - 1].Sum() % M;

			for (int j = 1; j <= n; j++)
			{
				dp[i][j] = sum;
				dp[i][j] -= dp[i - 1][j];
			}

			foreach (var (u, v) in es)
			{
				dp[i][u] -= dp[i - 1][v];
				dp[i][v] -= dp[i - 1][u];
			}

			for (int j = 1; j <= n; j++)
			{
				dp[i][j] = MInt(dp[i][j]);
			}
		}

		return dp[k][1];
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
