using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, c) = Read3();
		var a = Array.ConvertAll(new bool[h], _ => ReadL());

		const long max = 1L << 60;

		var r = Solve2();

		Array.ForEach(a, Array.Reverse);
		r = Math.Min(r, Solve2());

		return r;

		long Solve2()
		{
			var r = max;
			var dp = NewArray2(h, w, max);
			dp[0][0] = a[0][0];

			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					if (dp[i][j] != max) continue;

					var nv = a[i][j];
					var nr = max;
					if (i > 0)
					{
						nv = Math.Min(nv, dp[i - 1][j] + c);
						nr = Math.Min(nr, dp[i - 1][j] + c + a[i][j]);
					}
					if (j > 0)
					{
						nv = Math.Min(nv, dp[i][j - 1] + c);
						nr = Math.Min(nr, dp[i][j - 1] + c + a[i][j]);
					}
					dp[i][j] = Math.Min(dp[i][j], nv);
					r = Math.Min(r, nr);
				}
			}

			return r;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
