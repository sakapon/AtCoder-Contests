using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var rn = Enumerable.Range(0, n).ToArray();
		var rn1 = Enumerable.Range(0, n + 1).ToArray();

		// 0: 壁, 1: 空
		var c2 = Array.ConvertAll(c, s => rn.Sum(i => s[i] == '#' ? 0 : 1 << i));

		// 0: 使用済み, 1: 残り
		var dp = NewArray2<long>(1 << n, n + 1);
		dp[^1][0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x < 1 << n; x++)
			{
				AllSubsets(n, x, y =>
				{
					if (y == 0) return false;
					if ((c2[i] & y) != y) return false;

					var nj = BitOperations.PopCount((uint)y);
					for (int j = 0; j < n; j++)
					{
						dp[x & ~y][Math.Max(j, nj)] += dp[x][j];
					}
					return false;
				});
			}
		}

		return dp.Sum(r => rn1.Sum(j => j * r[j] % M)) % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static void AllSubsets(int n, int s, Func<int, bool> action)
	{
		for (int x = 0; ; x = (x - s) & s)
		{
			if (action(x)) break;
			if (x == s) break;
		}
	}
}
