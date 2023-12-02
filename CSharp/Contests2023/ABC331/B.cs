using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, s, m, l) = Read4();

		const int max = 1 << 30;

		var dp = new int[120];
		Array.Fill(dp, max);
		dp[0] = 0;

		for (int i = 0; i < 6; i++)
			for (int k = 0; 6 * (k + 1) + i < dp.Length; k++)
				Chmin(ref dp[6 * (k + 1) + i], dp[6 * k + i] + s);

		for (int i = 0; i < 8; i++)
			for (int k = 0; 8 * (k + 1) + i < dp.Length; k++)
				Chmin(ref dp[8 * (k + 1) + i], dp[8 * k + i] + m);

		for (int i = 0; i < 12; i++)
			for (int k = 0; 12 * (k + 1) + i < dp.Length; k++)
				Chmin(ref dp[12 * (k + 1) + i], dp[12 * k + i] + l);

		return dp[n..].Min();
	}

	public static int Chmin(ref int x, int v) => x > v ? x = v : x;
}
