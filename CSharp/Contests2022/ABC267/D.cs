using System;
using System.Collections.Generic;
using System.Linq;

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

		const long min = -1L << 60;
		var dp = new long[m + 1];
		var dt = new long[m + 1];
		Array.Fill(dp, min);
		dp[0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = 1; j <= m; j++)
			{
				dt[j] = dp[j];
				if (dp[j - 1] != min) dt[j] = Math.Max(dt[j], dp[j - 1] + j * a[i]);
			}

			(dp, dt) = (dt, dp);
		}
		return dp[m];
	}
}
