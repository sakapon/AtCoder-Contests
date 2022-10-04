using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var p = Read();
		var q = Read();

		var pr = Enumerable.Range(0, n).OrderBy(i => p[i]).ToArray();

		// i 番目まで、j 人を選び、選ばれない人のうちの 2 回目の最高順位 m
		var dp = NewArray2<long>(n + 1, n + 2);
		dp[0][n + 1] = 1;

		for (int i = 0; i < n; i++)
		{
			var dt = NewArray2<long>(n + 1, n + 2);
			var order = q[pr[i]];

			for (int j = 0; j < n; j++)
			{
				for (int m = 1; m <= n + 1; m++)
				{
					if (dp[j][m] == 0) continue;

					var nm = Math.Min(m, order);
					dt[j][nm] += dp[j][m];
					dt[j][nm] %= M;

					if (order <= m)
					{
						dt[j + 1][m] += dp[j][m];
						dt[j + 1][m] %= M;
					}
				}
			}
			dp = dt;
		}

		return dp[k].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
