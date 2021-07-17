using System;

class Q090D
{
	const long M = 998244353;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((int, int))Read2L();

		if (n > 100 || k > 100) throw new NotImplementedException();

		// dp[l][r][min], [l, r)
		var dp = NewArray3<long>(n + 1, n + 1, k + 2);

		for (int l = 0; l <= n; l++)
		{
			for (int m = 0; m <= k + 1; m++)
			{
				dp[l][l][m] = 1;
			}
		}

		for (int c = 1; c <= n; c++)
		{
			for (int l = 0; l + c <= n; l++)
			{
				var r = l + c;

				for (int m = k / c; m >= 0; m--)
				{
					for (int i = 0; i < c; i++)
					{
						dp[l][r][m] += dp[l][l + i][m] * dp[l + i + 1][r][m + 1];
						dp[l][r][m] %= M;
					}

					dp[l][r][m] += dp[l][r][m + 1];
					dp[l][r][m] %= M;
				}
			}
		}

		return dp[0][n][0];
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
