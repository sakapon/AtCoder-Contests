using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var (x, y) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int max = 1 << 30;
		var r = max;
		var dp = NewArray2(301, 301, max);
		dp[0][0] = 0;

		foreach (var (a, b) in ps)
		{
			for (int i = 300; i >= 0; i--)
			{
				for (int j = 300; j >= 0; j--)
				{
					if (dp[i][j] == max) continue;

					var count = dp[i][j] + 1;
					var ni = Math.Min(300, i + a);
					var nj = Math.Min(300, j + b);

					if (ni >= x && nj >= y)
					{
						r = Math.Min(r, count);
					}
					else
					{
						dp[ni][nj] = Math.Min(dp[ni][nj], count);
					}
				}
			}
		}

		if (r == max) return -1;
		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
