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

		var ps = new[] { (6, s), (8, m), (12, l) };
		const int max = 1 << 30;

		var dp = new int[120];
		Array.Fill(dp, max);
		dp[0] = 0;

		foreach (var (w, c) in ps)
			for (int r = 0; r < w; r++)
				for (int i = r; i + w < dp.Length; i += w)
					Chmin(ref dp[i + w], dp[i] + c);
		return dp[n..].Min();
	}

	public static int Chmin(ref int x, int v) => x > v ? x = v : x;
}
