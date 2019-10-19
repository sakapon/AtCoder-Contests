using System;

class M
{
	static void Main()
	{
		Func<int[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var h = read();
		var a = read();
		int n = h[0], k = h[1];

		var dp = new long[n + 1, k + 1];
		for (int i = 0; i <= n; i++) dp[i, 0] = 1;
		for (int j = 0; j <= k; j++) dp[0, j] = 1;
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= k; j++)
				dp[i, j] = MInt(dp[i, j - 1] + dp[i - 1, j] - (j > a[i - 1] ? dp[i - 1, j - a[i - 1] - 1] : 0));
		Console.WriteLine(MInt(dp[n, k] - (k > 0 ? dp[n, k - 1] : 0)));
	}

	const int m = 1000000007;
	static long MInt(long x) => (x %= m) < 0 ? x + m : x;
}
