using System;

class Q005
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, b, k) = ((long, int, int))Read3L();
		var cs = Read();

		// 10^(2^i) mod b
		var p10 = 10 % b;

		// 2^i 桁で j mod b
		var dp = new long[b];
		foreach (var c in cs)
			dp[c % b]++;

		var r = new long[b];
		r[0] = 1;

		for (; n != 0; dp = Multiply(dp, dp, p10), p10 = p10 * p10 % b, n >>= 1)
			if ((n & 1) != 0) r = Multiply(r, dp, p10);
		return r[0];

		// p10: 10^(2^i) on dp2
		long[] Multiply(long[] dp1, long[] dp2, long p10)
		{
			var m = new long[b];
			for (int j1 = 0; j1 < b; j1++)
			{
				for (int j2 = 0; j2 < b; j2++)
				{
					var nj = (p10 * j1 + j2) % b;
					m[nj] += dp1[j1] * dp2[j2];
					m[nj] %= M;
				}
			}
			return m;
		}
	}
}
