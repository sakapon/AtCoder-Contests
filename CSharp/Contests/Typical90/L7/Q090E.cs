using System;

class Q090E
{
	const long M = 998244353;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((int, int))Read2L();

		if (n > 10000 || k > 10000) throw new NotImplementedException();

		// dp[c][min]
		var dp = NewArray2<long>(n + 1, k + 2);

		for (int m = 0; m <= k + 1; m++)
		{
			dp[0][m] = 1;
		}

		for (int c = 1; c <= n; c++)
		{
			for (int m = k / c; m >= 0; m--)
			{
				// i 番目を a_i = m として追加
				for (int i = 0; i < c; i++)
				{
					dp[c][m] += dp[i][m] * dp[c - 1 - i][m + 1];
					dp[c][m] %= M;
				}

				dp[c][m] += dp[c][m + 1];
				dp[c][m] %= M;
			}
		}

		return dp[n][0];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
