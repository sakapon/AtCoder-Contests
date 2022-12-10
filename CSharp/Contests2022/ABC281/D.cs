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
		var (n, K, d) = Read3();
		var a = Read();

		// i: 項、j: mod, k: 個数
		// 値: 最大値
		var dp = NewArray2(d, K + 1, -1L);
		var dt = NewArray2(d, K + 1, -1L);
		dt[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < d; j++)
			{
				for (int k = 0; k <= K; k++)
				{
					dp[j][k] = dt[j][k];
				}
			}

			var v = a[i];

			for (int j = 0; j < d; j++)
			{
				var nj = (j + v) % d;

				for (int k = 0; k < K; k++)
				{
					if (dp[j][k] == -1) continue;

					var nv = dp[j][k] + v;
					dt[nj][k + 1] = Math.Max(dt[nj][k + 1], nv);
				}
			}
		}
		return dt[0][K];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
