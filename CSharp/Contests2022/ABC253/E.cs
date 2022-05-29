using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();

		var dp = NewArray2<long>(n, m + 1);
		Array.Fill(dp[0], 1);
		dp[0][0] = 0;

		for (int i = 1; i < n; i++)
		{
			var rsq = new StaticRSQ1(dp[i - 1]);
			var s = rsq.Raw[^1];

			for (int j = 1; j <= m; j++)
			{
				if (k == 0)
				{
					dp[i][j] = s % M;
				}
				else
				{
					dp[i][j] = (s - rsq.GetSum(j - k + 1, j + k)) % M;
				}
			}
		}

		return dp[^1].Sum() % M;
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
