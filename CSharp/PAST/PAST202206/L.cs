using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();
		var K = int.Parse(Console.ReadLine());

		var n = s.Length;
		var m = t.Length;

		var dp = NewArray3<long>(n + 1, m + 1, K + 1);

		for (int i = 0; i < n; ++i)
		{
			for (int j = 0; j < m; ++j)
			{
				if (s[i] == t[j])
				{
					for (int k = 0; k <= K; k++)
					{
						dp[i + 1][j + 1][k] = dp[i][j][k] + 1;
					}
				}
				else
				{
					dp[i + 1][j + 1][0] = Math.Max(dp[i + 1][j][0], dp[i][j + 1][0]);
					for (int k = 1; k <= K; k++)
					{
						var v = Math.Max(dp[i + 1][j][k], dp[i][j + 1][k]);
						dp[i + 1][j + 1][k] = Math.Max(v, dp[i][j][k - 1] + 1);
					}
				}
			}
		}
		return dp[n][m].Max();
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
