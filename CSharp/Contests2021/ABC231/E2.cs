using System;

class E2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var a = ReadL();

		const long max = 1L << 60;
		var dp = NewArray2((int)n + 1, 2, max);
		dp[0][0] = 0;

		long GetValue(int i, int j) => x / a[i] * a[i] + a[i] * j;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				var y = GetValue(i, j);
				var v = dp[i][j];
				if (v == max) continue;

				if (i < n - 1)
				{
					var rem = y % a[i + 1];
					dp[i + 1][rem == 0 && x < y ? 1 : 0] = Math.Min(dp[i + 1][rem == 0 && x < y ? 1 : 0], v + rem / a[i]);
					dp[i + 1][1] = Math.Min(dp[i + 1][1], v + (a[i + 1] - rem) / a[i]);
				}
				else
				{
					dp[n][0] = Math.Min(dp[n][0], v + y / a[i]);
				}
			}
		}

		return dp[n][0];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
