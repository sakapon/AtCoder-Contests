using System;

// Test: https://atcoder.jp/contests/abc180/tasks/abc180_e
namespace CoderLib6.Graphs
{
	public static class TSP
	{
		public static T[] NewArray1<T>(int n, T v = default(T)) => Array.ConvertAll(new bool[n], _ => v);
		public static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

		public static long[][] Execute(int n, int sv, long[][] d)
		{
			// i: 訪問済の頂点集合
			// j: 最後の頂点
			var dp = NewArray2(1 << n, n, long.MaxValue);
			dp[0][sv] = 0;

			for (uint x = 0; x < 1U << n; ++x)
			{
				for (int v = 0; v < n; ++v)
				{
					if (dp[x][v] == long.MaxValue) continue;

					for (int nv = 0; nv < n; ++nv)
					{
						var nx = x | (1U << nv);
						if (nx == x) continue;
						if (d[v][nv] == long.MaxValue) continue;

						var nc = dp[x][v] + d[v][nv];
						if (dp[nx][nv] <= nc) continue;
						dp[nx][nv] = nc;
					}
				}
			}
			return dp;
		}
	}
}
