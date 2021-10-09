using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var dp = NewArray2<long>(n + 1, 3000 + 1);
		dp[0][0] = 1;

		for (int i = 1; i <= n; i++)
		{
			var av = a[i - 1];
			var bv = b[i - 1];

			var rsq = new StaticRSQ1(dp[i - 1]);

			for (int j = av; j <= bv; j++)
			{
				dp[i][j] = rsq.GetSum(0, j + 1) % M;
			}
		}

		return dp[n].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
