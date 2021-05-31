using System;

class Q050
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l) = Read2();

		var dp = new long[n + l];
		dp[0] = 1;

		for (int i = 0; i < n; i++)
		{
			dp[i + 1] += dp[i];
			dp[i + 1] %= M;
			dp[i + l] += dp[i];
			dp[i + l] %= M;
		}
		return dp[n];
	}
}
