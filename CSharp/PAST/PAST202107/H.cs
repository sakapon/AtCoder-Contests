using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	const double max = double.MaxValue;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int p, int t) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var n1 = n / 2;
		var n2 = n - 1 - n1;
		var sum = a.Sum();

		if (sum == 0) return n - 1;

		// k: sum
		var dp = NewArray3(n1 + 1, sum + 1, sum + 1, max);
		dp[0][0][0] = 0;

		// 広義単調増加
		for (int i = 0; i < n1; i++)
		{
			for (int j = 0; j < sum; j++)
			{
				for (int k = 0; k <= sum; k++)
				{
					if (dp[i][j][k] == max) continue;

					for (int nj = j; nj <= sum; nj++)
					{
						var nk = k + nj;
						if (nk > sum) break;

						var d = nj - j;
						dp[i + 1][nj][nk] = Math.Min(dp[i + 1][nj][nk], dp[i][j][k] + Math.Sqrt(d * d + 1));
					}
				}
			}
		}

		var r = max;

		for (int j = 0; j <= sum; j++)
		{
			for (int k1 = 0; k1 <= sum; k1++)
			{
				for (int k2 = 0; k2 <= sum; k2++)
				{
					if (k1 + k2 - j != sum) continue;
					if (dp[n1][j][k1] == max || dp[n2][j][k2] == max) continue;

					r = Math.Min(r, dp[n1][j][k1] + dp[n2][j][k2]);
				}
			}
		}

		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
