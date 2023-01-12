using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var nm = n + m;
		var ps = Array.ConvertAll(new bool[nm], _ => Read2L());

		var f = (1 << n) - 1;
		const double max = double.MaxValue;
		var dp = NewArray2(1 << nm, nm, max);
		for (int i = 0; i < nm; i++)
		{
			dp[1 << i][i] = Math.Sqrt(ps[i].x * ps[i].x + ps[i].y * ps[i].y);
		}

		for (int x = 0; x < 1 << nm; x++)
		{
			var vel = 1 << BitOperations.PopCount((uint)x >> n);
			for (int v = 0; v < nm; v++)
			{
				if (dp[x][v] == max) continue;

				for (int nv = 0; nv < nm; nv++)
				{
					if ((x & (1 << nv)) != 0) continue;
					dp[x | (1 << nv)][nv] = Math.Min(dp[x | (1 << nv)][nv], dp[x][v] + Dist(v, nv) / vel);
				}
			}
		}

		var r = max;
		for (int x = 0; x < 1 << nm; x++)
		{
			if ((x & f) != f) continue;
			var vel = 1 << BitOperations.PopCount((uint)x >> n);

			for (int v = 0; v < nm; v++)
			{
				r = Math.Min(r, dp[x][v] + Math.Sqrt(ps[v].x * ps[v].x + ps[v].y * ps[v].y) / vel);
			}
		}
		return r;

		double Dist(int i, int j)
		{
			var dx = ps[i].x - ps[j].x;
			var dy = ps[i].y - ps[j].y;
			return Math.Sqrt(dx * dx + dy * dy);
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
