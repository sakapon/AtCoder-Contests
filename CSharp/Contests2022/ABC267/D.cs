using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		var dp = new long[m + 1];
		Array.Fill(dp, -1L << 60);
		dp[0] = 0;

		for (int i = 0; i < n; i++)
			for (int j = m; j > 0; j--)
				dp[j] = Math.Max(dp[j], dp[j - 1] + j * a[i]);
		return dp[m];
	}
}
