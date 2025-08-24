using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n + m], _ => Read2L());

		var nm = n + m + 1;
		ps = ps.Prepend((0, 0)).ToArray();

		var d = NewArray2(nm, nm, double.MaxValue);
		for (int i = 0; i < nm; i++)
			for (int j = 0; j < nm; j++)
				d[i][j] = Dist(i, j);

		var sv = 0;
		var dp = TSP(nm, sv, d);

		var f = (1 << n + 1) - 1;
		return Enumerable.Range(0, 1 << nm).Where(x => (x & f) == f).Min(x => dp[x][sv]);

		static double Norm(long dx, long dy) => Math.Sqrt(dx * dx + dy * dy);
		double Dist(int i, int j)
		{
			var dx = ps[i].x - ps[j].x;
			var dy = ps[i].y - ps[j].y;
			return Norm(dx, dy);
		}

		double[][] TSP(int nm, int sv, double[][] d)
		{
			// dp[訪問済の頂点集合][最後に訪問した頂点]: 最短距離
			var dp = NewArray2(1 << nm, nm, double.MaxValue);
			// ここでは、始点を未訪問とします。
			dp[0][sv] = 0;

			for (uint x = 0; x < 1U << nm; ++x)
			{
				var vel = 1 << BitOperations.PopCount(x >> n + 1);

				for (int v = 0; v < nm; ++v)
				{
					if (dp[x][v] == double.MaxValue) continue;

					for (int nv = 0; nv < nm; ++nv)
					{
						var nx = x | (1U << nv);
						if (nx == x) continue;
						if (d[v][nv] == double.MaxValue) continue;

						var nc = dp[x][v] + d[v][nv] / vel;
						if (dp[nx][nv] <= nc) continue;
						dp[nx][nv] = nc;
					}
				}
			}
			return dp;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
