using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var set = Array.ConvertAll(new bool[m], _ => Read2L()).ToHashSet();

		// (a, b) を i 回、(c, d) を j 回、(e, f) を k 回
		var dp = NewArray3<long>(n + 1, n + 1, n + 1);
		dp[0][0][0] = 1;

		for (int d = 0; d < n; d++)
		{
			for (int i = 0; i <= d; i++)
			{
				var d2 = d - i;
				for (int j = 0; j <= d2; j++)
				{
					var k = d2 - j;
					if (!set.Contains((a[0] * (i + 1) + a[2] * j + a[4] * k, a[1] * (i + 1) + a[3] * j + a[5] * k)))
					{
						dp[i + 1][j][k] += dp[i][j][k];
						dp[i + 1][j][k] %= M;
					}
					if (!set.Contains((a[0] * i + a[2] * (j + 1) + a[4] * k, a[1] * i + a[3] * (j + 1) + a[5] * k)))
					{
						dp[i][j + 1][k] += dp[i][j][k];
						dp[i][j + 1][k] %= M;
					}
					if (!set.Contains((a[0] * i + a[2] * j + a[4] * (k + 1), a[1] * i + a[3] * j + a[5] * (k + 1))))
					{
						dp[i][j][k + 1] += dp[i][j][k];
						dp[i][j][k + 1] %= M;
					}
				}
			}
		}

		var r = 0L;
		for (int i = 0; i <= n; i++)
		{
			var d2 = n - i;
			for (int j = 0; j <= d2; j++)
			{
				var k = d2 - j;
				r += dp[i][j][k];
			}
		}
		return r % M;
	}

	const long M = 998244353;
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
