using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		int ToBits(int i, int min)
		{
			var p = ps[i];
			var r = 0;

			for (int f = 0; f < 5; f++)
			{
				if (p[f] >= min)
				{
					r |= 1 << f;
				}
			}
			return r;
		}

		return Max(-1, max, v =>
		{
			var dp = Array.ConvertAll(new bool[1 << 5], _ => max);
			dp[0] = 0;

			for (int i = 0; i < n; i++)
			{
				var y = ToBits(i, v);

				for (int x = dp.Length - 1; x >= 0; x--)
				{
					if (dp[x] == max) continue;
					var nx = x | y;
					dp[nx] = Math.Min(dp[nx], dp[x] + 1);
				}
			}
			return dp[^1] <= 3;
		});
	}

	static int Max(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
