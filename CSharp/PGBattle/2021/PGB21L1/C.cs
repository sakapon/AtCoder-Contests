using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, a, b) = Read4();

		var dp = NewArray2<long>(n + 1, m + 1);
		dp[0][0] = 1;

		for (int i = 0; i <= n; i++)
		{
			for (int j = 0; j <= m; j++)
			{
				if ((i, j) == (a, b)) continue;

				if (j < m)
				{
					dp[i][j + 1] += dp[i][j];
					dp[i][j + 1] %= M;
				}
				if (i < n)
				{
					dp[i + 1][j] += dp[i][j];
					dp[i + 1][j] %= M;
				}
			}
		}
		return dp[n][m];
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
