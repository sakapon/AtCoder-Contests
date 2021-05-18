using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		var dp = new long[n];
		dp[1] = 1;

		for (int i = 2; i < n; i++)
			dp[i] = (dp[i - 2] + dp[i - 1]) % m;
		return dp[^1];
	}
}
