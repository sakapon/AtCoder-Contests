using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, W) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		return DP1(n, W, ps);
	}

	// 配る (2 次元)
	static long DP1(int n, int W, (int, int)[] ps)
	{
		var dp = new long[n + 1][];
		dp[0] = new long[W + 1];
		Array.Fill(dp[0], -1);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			dp[i + 1] = (long[])dp[i].Clone();
			var (w, v) = ps[i];

			for (int j = 0; j + w <= W; j++)
			{
				if (dp[i][j] == -1) continue;
				Chmax(ref dp[i + 1][j + w], dp[i][j] + v);
			}
		}
		return dp[n].Max();
	}

	// 貰う (2 次元)
	static long DP2(int n, int W, (int, int)[] ps)
	{
		var dp = new long[n + 1][];
		dp[0] = new long[W + 1];
		Array.Fill(dp[0], -1);
		dp[0][0] = 0;

		for (int i = 1; i <= n; i++)
		{
			dp[i] = new long[W + 1];
			var (w, v) = ps[i - 1];

			for (int j = 0; j <= W; j++)
			{
				dp[i][j] = Math.Max(dp[i - 1][j], j >= w ? dp[i - 1][j - w] + v : -1);
			}
		}
		return dp[n].Max();
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
