using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var dp = NewArray2(a + 1, b + 1, -1L);
		dp[0][0] = 0;

		foreach (var (w, v) in ps)
		{
			for (int i = a; i >= 0; i--)
			{
				for (int j = b; j >= 0; j--)
				{
					if (dp[i][j] == -1) continue;

					if (i + w <= a) ChFirstMax(ref dp[i + w][j], dp[i][j] + v);
					if (j + w <= b) ChFirstMax(ref dp[i][j + w], dp[i][j] + v);
				}
			}
		}
		return dp.Max(r => r.Max());
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
