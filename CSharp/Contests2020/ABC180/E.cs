using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y, int z) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var d = TSP.NewArray2<long>(n, n);

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				d[i][j] = Math.Abs(ps[j].x - ps[i].x) + Math.Abs(ps[j].y - ps[i].y) + Math.Max(0, ps[j].z - ps[i].z);
			}
		}

		var dp = TSP.Execute(n, 0, d);
		return Enumerable.Range(1, n - 1).Min(v => dp[(1 << n) - 1][v] + d[v][0]);
	}
}

public static class TSP
{
	public static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static long[][] Execute(int n, int sv, long[][] d)
	{
		// i: 訪問済の頂点集合
		// j: 最後の頂点
		var dp = NewArray2(1 << n, n, long.MaxValue);
		dp[1 << sv][sv] = 0;

		for (uint x = 0; x < 1U << n; ++x)
		{
			for (int j = 0; j < n; ++j)
			{
				if (dp[x][j] == long.MaxValue) continue;

				for (int nj = 0; nj < n; ++nj)
				{
					var nx = x | (1U << nj);
					if (nx == x) continue;
					if (d[j][nj] == long.MaxValue) continue;
					var nv = dp[x][j] + d[j][nj];
					if (dp[nx][nj] > nv) dp[nx][nj] = nv;
				}
			}
		}
		return dp;
	}
}
