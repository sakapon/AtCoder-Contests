using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, K, d) = Read3();
		var a = Read();

		// i: 項、j: 個数、k: mod
		// 値: 最大値
		var dp = NewArray2(K + 1, d, -1L);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			var v = a[i];

			for (int j = K - 1; j >= 0; j--)
			{
				for (int k = 0; k < d; k++)
				{
					if (dp[j][k] == -1) continue;

					var nk = (k + v) % d;
					var nv = dp[j][k] + v;
					ChFirstMax(ref dp[j + 1][nk], nv);
				}
			}
		}
		return dp[K][0];
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
