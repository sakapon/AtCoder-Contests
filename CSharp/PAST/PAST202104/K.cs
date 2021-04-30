using System;
using System.Collections.Generic;
using System.Linq;

class K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int min = int.MinValue;
		var dp = NewArray2(n + 1, 100, min);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			var (p, u) = ps[i];

			for (int j = 0; j < 100; j++)
			{
				if (dp[i][j] == min) continue;

				dp[i + 1][j] = Math.Max(dp[i + 1][j], dp[i][j]);

				var r = Math.DivRem(j + p, 100, out var nj);
				dp[i + 1][nj] = Math.Max(dp[i + 1][nj], dp[i][j] + u - p + 20 * r);
			}
		}
		return dp[n].Max();
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
