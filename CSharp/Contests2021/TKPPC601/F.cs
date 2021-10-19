using System;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();

		var p2k = MPow(2, k);

		var dp = new long[n + 1, 2];
		dp[1, 0] = 1;

		for (int i = 2; i <= n; i++)
		{
			dp[i, 0] = dp[i - 1, 0] + dp[i - 1, 1];
			dp[i, 0] %= M;

			dp[i, 1] = dp[i, 0];
			dp[i, 1] *= 2;
			if (i - k > 0)
				dp[i, 1] -= dp[i - k, 0] * p2k;
			dp[i, 1] = MInt(dp[i, 1]);
		}

		return dp[n, 0];
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
