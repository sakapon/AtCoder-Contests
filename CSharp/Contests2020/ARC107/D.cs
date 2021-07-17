using System;

class D
{
	const long M = 998244353;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();

		var dp = new long[n + 1, n + 1];
		dp[0, 0] = 1;

		for (int i = 1; i <= n; i++)
		{
			for (int j = i; j > 0; j--)
			{
				dp[i, j] = dp[i - 1, j - 1];
				if (2 * j <= i) dp[i, j] += dp[i, 2 * j];
				dp[i, j] %= M;
			}
		}
		Console.WriteLine(dp[n, k]);
	}
}
