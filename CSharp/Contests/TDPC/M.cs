using System;
using System.Linq;
using CoderLib6.Values;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, n) = Read2();
		var g = Array.ConvertAll(new bool[n], _ => Read());

		const int M = 1000000007;
		var rn = Enumerable.Range(0, n).ToArray();

		long[] Comb(int sv)
		{
			// i: 最後の部屋、j: 訪問済の部屋の集合
			var dp = NewArray2<long>(n, 1 << n);
			dp[sv][1 << sv] = 1;

			for (int x = 1; x < 1 << n; x++)
			{
				for (int i = 0; i < n; i++)
				{
					if (dp[i][x] == 0) continue;

					for (int j = 0; j < n; j++)
					{
						if (g[i][j] == 0) continue;
						var nx = x | (1 << j);
						if (nx == x) continue;
						dp[j][nx] += dp[i][x];
					}
				}
			}
			return Array.ConvertAll(dp, a => a.Sum() % M);
		}

		var m = new long[n, n];

		for (int i = 0; i < n; i++)
		{
			var c = Comb(i);
			for (int j = 0; j < n; j++)
				m[i, j] = c[j];
		}

		var dp = new long[n];
		dp[0] = 1;

		m = ModuloMatrixHelper.MPow(m, h);
		dp = ModuloMatrixHelper.MMul(m, dp);
		return dp[0];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
