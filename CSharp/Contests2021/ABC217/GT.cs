using System;

class GT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		// TLE
		var qs = new int[m];
		for (int i = 0; i < n; i++)
			qs[i % m]++;

		var mc = new MCombination(n + 1);

		var dp = NewArray2<long>(m + 1, n + 1);
		dp[0][0] = 1;

		for (int i = 1; i <= m; i++)
		{
			var q = qs[i - 1];
			for (int j = 0; j <= n; j++)
			{
				for (int k = 0; k <= j; k++)
				{
					var d = j - k;
					if (d <= q && q <= j)
					{
						dp[i][j] += dp[i - 1][k] * mc.MNcr(q, d) % M * mc.MNpr(k, q - d);
						dp[i][j] %= M;
					}
				}
			}
		}

		return string.Join("\n", dp[^1][1..]);
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
