using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var r = ReadL();
		var c = ReadL();
		var a = Array.ConvertAll(new bool[h], _ => Console.ReadLine().Select(x => x == '1').ToArray());

		// 0: 反転なし
		// 1: r 反転
		// 2: c 反転
		// 3: rc 反転
		var dp = NewArray3(h, w, 4, max);
		dp[0][0][0] = 0;
		dp[0][0][1] = r[0];
		dp[0][0][2] = c[0];
		dp[0][0][3] = r[0] + c[0];

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (i > 0)
				{
					if (a[i][j] == a[i - 1][j])
					{
						SetMin3(dp, i, j, 0, dp[i - 1][j][0]);
						SetMin3(dp, i, j, 1, dp[i - 1][j][1] + r[i]);
						SetMin3(dp, i, j, 2, dp[i - 1][j][2]);
						SetMin3(dp, i, j, 3, dp[i - 1][j][3] + r[i]);
					}
					else
					{
						SetMin3(dp, i, j, 0, dp[i - 1][j][1]);
						SetMin3(dp, i, j, 1, dp[i - 1][j][0] + r[i]);
						SetMin3(dp, i, j, 2, dp[i - 1][j][3]);
						SetMin3(dp, i, j, 3, dp[i - 1][j][2] + r[i]);
					}
				}

				if (j > 0)
				{
					if (a[i][j] == a[i][j - 1])
					{
						SetMin3(dp, i, j, 0, dp[i][j - 1][0]);
						SetMin3(dp, i, j, 1, dp[i][j - 1][1]);
						SetMin3(dp, i, j, 2, dp[i][j - 1][2] + c[j]);
						SetMin3(dp, i, j, 3, dp[i][j - 1][3] + c[j]);
					}
					else
					{
						SetMin3(dp, i, j, 0, dp[i][j - 1][2]);
						SetMin3(dp, i, j, 1, dp[i][j - 1][3]);
						SetMin3(dp, i, j, 2, dp[i][j - 1][0] + c[j]);
						SetMin3(dp, i, j, 3, dp[i][j - 1][1] + c[j]);
					}
				}
			}
		}
		return dp[^1][^1].Min();
	}

	const long max = 1L << 60;
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
	static void SetMin3(long[][][] a, int i, int j, int k, long v) { if (a[i][j][k] > v) a[i][j][k] = v; }
}
