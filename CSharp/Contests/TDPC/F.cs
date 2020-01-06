using System;

class F
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		var dp = new long[n + 1, 3];
		dp[1, 0] = 1;

		for (int i = 1; i < n; i++)
		{
			dp[i + 1, 0] = dp[i, 2];
			dp[i + 1, 1] = MInt(dp[i, 0] + dp[i, 1] - (i > k - 2 ? dp[i - k + 2, 0] : 0));
			dp[i + 1, 2] = MInt(dp[i, 0] + dp[i, 1] + dp[i, 2]);
		}
		Console.WriteLine(MInt(dp[n, 0] + dp[n, 1]));
	}

	const int M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
