using System;

class F
{
	static void Main()
	{
		var M = 1000000007;
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1], n2 = n * n;

		var dp = new long[n + 1, n + 1, n2 + 1];
		dp[0, 0, 0] = 1;
		for (int i = 0; i < n; i++)
			for (int j = 0; j <= n; j++)
				for (int t, x = 0; x <= n2; x++)
				{
					if (dp[i, j, x] == 0) continue;
					if (j > 0 && (t = x + 2 * j - 2) <= n2)
					{
						dp[i + 1, j - 1, t] += j * j * dp[i, j, x];
						dp[i + 1, j - 1, t] %= M;
					}
					if ((t = x + 2 * j) <= n2)
					{
						dp[i + 1, j, t] += (2 * j + 1) * dp[i, j, x];
						dp[i + 1, j, t] %= M;
					}
					if ((t = x + 2 * j + 2) <= n2)
					{
						dp[i + 1, j + 1, t] += dp[i, j, x];
						dp[i + 1, j + 1, t] %= M;
					}
				}
		Console.WriteLine(dp[n, 0, k]);
	}
}
