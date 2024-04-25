using System;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, W) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const long Invalid = -1;

		// dp[i][j]: 品物 i までを選んで重さが j のときの価値の最大値
		var dp = new long[n + 1, W + 1];
		for (int j = 1; j <= W; j++)
			dp[0, j] = long.MinValue;
		for (int i = 1; i <= n; i++)
			for (int j = 0; j <= W; j++)
				dp[i, j] = Invalid;

		return Enumerable.Range(0, W + 1).Max(j => Rec(n, j));

		// メモ化再帰
		long Rec(int i, int j)
		{
			if (dp[i, j] != Invalid) return dp[i, j];

			var (w, v) = ps[i - 1];
			return dp[i, j] = Math.Max(Rec(i - 1, j), j >= w ? Rec(i - 1, j - w) + v : long.MinValue);
		}
	}
}
