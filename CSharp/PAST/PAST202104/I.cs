using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => ReadL());

		const int none = -1;
		var dp = NewArray3<long>(h, w, h + w, none);
		dp[0][0][0] = 0;
		dp[0][0][1] = a[0][0];

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (i > 0)
				{
					for (int k = 0; k < h + w; k++)
					{
						if (dp[i - 1][j][k] == none) break;
						dp[i][j][k] = Math.Max(dp[i][j][k], dp[i - 1][j][k]);
					}
					for (int k = 1; k < h + w; k++)
					{
						if (dp[i - 1][j][k - 1] == none) break;
						dp[i][j][k] = Math.Max(dp[i][j][k], dp[i - 1][j][k - 1] + a[i][j]);
					}
				}

				if (j > 0)
				{
					for (int k = 0; k < h + w; k++)
					{
						if (dp[i][j - 1][k] == none) break;
						dp[i][j][k] = Math.Max(dp[i][j][k], dp[i][j - 1][k]);
					}
					for (int k = 1; k < h + w; k++)
					{
						if (dp[i][j - 1][k - 1] == none) break;
						dp[i][j][k] = Math.Max(dp[i][j][k], dp[i][j - 1][k - 1] + a[i][j]);
					}
				}
			}
		}

		return string.Join("\n", Enumerable.Range(1, h + w - 1).Select(k => dp[^1][^1][k]));
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
