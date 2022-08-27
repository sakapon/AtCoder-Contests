using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var set = Array.ConvertAll(new bool[m], _ => Read2L()).ToHashSet();

		(long, long) f(int i, int j, int k) => (a[0] * i + a[2] * j + a[4] * k, a[1] * i + a[3] * j + a[5] * k);

		// (a, b) を i 回、(c, d) を j 回
		var dp = new long[n + 1, n + 1];
		var dt = new long[n + 1, n + 1];
		dp[0, 0] = 1;

		for (int d = 0; d < n; d++)
		{
			for (int i = 0; i <= d; i++)
			{
				var d2 = d - i;
				for (int j = 0; j <= d2; j++)
				{
					if (dp[i, j] == 0) continue;
					var k = d2 - j;

					if (!set.Contains(f(i + 1, j, k)))
					{
						dt[i + 1, j] += dp[i, j];
						dt[i + 1, j] %= M;
					}
					if (!set.Contains(f(i, j + 1, k)))
					{
						dt[i, j + 1] += dp[i, j];
						dt[i, j + 1] %= M;
					}
					if (!set.Contains(f(i, j, k + 1)))
					{
						dt[i, j] += dp[i, j];
						dt[i, j] %= M;
					}
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		var r = 0L;
		for (int i = 0; i <= n; i++)
		{
			for (int j = 0; j <= n; j++)
			{
				r += dp[i, j];
			}
		}
		return r % M;
	}

	const long M = 998244353;
}
