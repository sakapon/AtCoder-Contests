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
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine().Select(c => c == '#').ToArray());

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (!s[i][j]) continue;

				if (i + 1 < h) s[i + 1][j] = true;
				if (i + 1 < h && j + 1 < w) s[i + 1][j + 1] = true;
			}
		}

		var dp = new long[h + 2];
		var dt = new long[h + 2];

		for (int i = h; i >= 0; i--)
		{
			dp[i] = 1;
		}

		for (int j = 0; j < w; j++)
		{
			for (int i = h - 1; i >= 0; i--)
			{
				if (s[i][j])
				{
					dt[i + 2] = 0;
				}
				else
				{
					dt[i + 2] = dp[i + 1];
					if (i < h - 1) dt[i + 2] += dt[i + 3];
					dt[i + 2] %= M;
				}
			}
			dt[0] = dt[1] = (dp[0] + dt[2]) % M;

			(dp, dt) = (dt, dp);
		}

		return dp[0];
	}

	const long M = 998244353;
}
