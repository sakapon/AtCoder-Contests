using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p) = Read2();

		// j 本の辺を取り除く方法
		// k=0: 連結
		// k=1: 非連結、二型
		// k=2: 非連結、＿または￣型 (したがって2倍)
		var dp = NewArray2<long>(n + 2, 3);
		dp[0][0] = 1;
		dp[1][1] = 1;

		for (int i = 1; i < n; i++)
		{
			var t = NewArray2<long>(n + 2, 3);

			for (int j = 0; j < n; j++)
			{
				t[j][0] += dp[j][0] + dp[j][1] + dp[j][2];
				t[j + 1][0] += dp[j][0] * 3;
				t[j + 1][1] += dp[j][1] + dp[j][2];
				t[j + 2][2] += dp[j][0] * 2;

				t[j][0] %= p;
				t[j][1] %= p;
				t[j][2] %= p;
			}

			(dp, t) = (t, dp);
		}
		return string.Join(" ", Enumerable.Range(1, n - 1).Select(j => dp[j][0]));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
