using System;
using System.Numerics;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = ((int, long, long))Read3L();
		var a = ReadL();
		var b = ReadL();

		var dp = new long[1 << n];
		Array.Fill(dp, 1L << 60);
		dp[0] = 0;

		for (uint s = 0; s < 1U << n; s++)
		{
			var i = BitOperations.PopCount(s);

			for (int j = 0; j < n; j++)
			{
				var ns = s | (1U << j);
				if (ns == s) continue;

				// j はフラグ 0 のビットのうち何番目か
				var k = j - BitOperations.PopCount(s & ((1U << j) - 1));
				dp[ns] = Math.Min(dp[ns], dp[s] + y * k + x * Math.Abs(a[j] - b[i]));
			}
		}

		return dp[^1];
	}
}
