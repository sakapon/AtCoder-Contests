using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, k) = Read3();
		var (x1, y1, x2, y2) = Read4();

		// 0: 終点
		// 1: 終点と同じ行
		// 2: 終点と同じ列
		// 3: それ以外
		var dp = NewArray2<long>(k + 1, 4);

		if ((x1, y1) == (x2, y2))
		{
			dp[0][0] = 1;
		}
		else if (x1 == x2)
		{
			dp[0][1] = 1;
		}
		else if (y1 == y2)
		{
			dp[0][2] = 1;
		}
		else
		{
			dp[0][3] = 1;
		}

		for (int i = 0; i < k; i++)
		{
			dp[i + 1][1] += dp[i][0] * (w - 1);
			dp[i + 1][2] += dp[i][0] * (h - 1);

			dp[i + 1][0] += dp[i][1];
			dp[i + 1][1] += dp[i][1] * (w - 2);
			dp[i + 1][3] += dp[i][1] * (h - 1);

			dp[i + 1][0] += dp[i][2];
			dp[i + 1][2] += dp[i][2] * (h - 2);
			dp[i + 1][3] += dp[i][2] * (w - 1);

			dp[i + 1][1] += dp[i][3];
			dp[i + 1][2] += dp[i][3];
			dp[i + 1][3] += dp[i][3] * (h + w - 4);

			for (int j = 0; j < 4; j++)
				dp[i + 1][j] %= M;
		}
		return dp[k][0];
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
