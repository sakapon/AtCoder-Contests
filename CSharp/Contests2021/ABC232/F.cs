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

		// 集合 s を数列の前方に移動・一致させるための費用
		var dp = new long[1 << n];
		Array.Fill(dp, long.MaxValue);
		dp[0] = 0;

		for (uint s = 0; s < 1U << n; s++)
		{
			var c = BitOperations.PopCount(s);

			for (int i = 0; i < n; i++)
			{
				var ns = s | (1U << i);
				if (ns == s) continue;

				// 移動量
				var d = BitOperations.PopCount(((1U << i) - 1) & ~s);

				var nv = dp[s] + d * y + Math.Abs(a[i] - b[c]) * x;
				if (dp[ns] > nv) dp[ns] = nv;
			}
		}
		return dp[^1];
	}
}
