using System;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, K) = Read3();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		const long max = 1L << 60;
		var r = max;

		foreach (var x in a.SelectMany(b => b))
		{
			// dp[i][j][k]: x 以上の値を k 個使ったときの和
			var dp = NewArray3(h, w, K + 2, max);
			if (a[0][0] <= x)
			{
				dp[0][0][0] = 0;
			}
			if (a[0][0] >= x)
			{
				dp[0][0][1] = a[0][0];
			}

			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					for (int k = 0; k <= K; k++)
					{
						if (dp[i][j][k] == max) continue;

						if (i + 1 < h)
						{
							var nk = k;
							var nv = dp[i][j][k];

							if (a[i + 1][j] <= x)
							{
								dp[i + 1][j][nk] = Math.Min(dp[i + 1][j][nk], nv);
							}
							if (a[i + 1][j] >= x)
							{
								nk++;
								nv += a[i + 1][j];
								dp[i + 1][j][nk] = Math.Min(dp[i + 1][j][nk], nv);
							}
						}

						if (j + 1 < w)
						{
							var nk = k;
							var nv = dp[i][j][k];

							if (a[i][j + 1] <= x)
							{
								dp[i][j + 1][nk] = Math.Min(dp[i][j + 1][nk], nv);
							}
							if (a[i][j + 1] >= x)
							{
								nk++;
								nv += a[i][j + 1];
								dp[i][j + 1][nk] = Math.Min(dp[i][j + 1][nk], nv);
							}
						}
					}
				}
			}

			r = Math.Min(r, dp[^1][^1][K]);
		}

		return r;
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
