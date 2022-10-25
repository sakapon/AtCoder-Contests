using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class D2
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

		// 0: 空, 1: 壁
		var c2 = Array.ConvertAll(c, s => rn.Sum(i => s[i] == '.' ? 0 : 1 << i));

		// 0: 空, 1: 使用済み
		var dp = NewArray2<long>(1 << n, n + 1);
		dp[0][0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int x = (1 << n) - 1; x >= 0; x--)
			{
				AllSupersets(n, x, y =>
				{
					if (y == x) return false;
					if ((c2[i] & y & ~x) != 0) return false;

					var nj = BitOperations.PopCount((uint)(y & ~x));
					for (int j = 0; j < n; j++)
					{
						dp[y][Math.Max(j, nj)] += dp[x][j];
					}
					return false;
				});
			}
		}

		return dp.Sum(r => rn1.Sum(j => j * r[j] % M)) % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static void AllSupersets(int n, int s, Func<int, bool> action)
	{
		for (int x = s; x < 1 << n; x = (x + 1) | s)
		{
			if (action(x)) break;
		}
	}
}
