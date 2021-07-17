using System;

namespace CoderLib8.Extra
{
	public static class SequenceHelper
	{
		// Longest Increasing Subsequence
		// 部分列の i 番目となりうる最小値 (0-indexed)。
		public static int[] Lis(int[] a)
		{
			var n = a.Length;
			var r = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);
			for (int i = 0; i < n; ++i)
				r[Min(0, n, x => r[x] >= a[i])] = a[i];
			return r;

			// 最大長を求める場合。
			//return Array.IndexOf(r, int.MaxValue);
		}

		static int Min(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		// Longest Common Subsequence
		public static int[,] Lcs<T>(T[] a, T[] b)
		{
			var n = a.Length;
			var m = b.Length;
			var c = System.Collections.Generic.EqualityComparer<T>.Default;

			var dp = new int[n + 1, m + 1];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					dp[i + 1, j + 1] = c.Equals(a[i], b[j]) ? dp[i, j] + 1 : Math.Max(dp[i + 1, j], dp[i, j + 1]);
			return dp;

			// 最大長を求める場合。
			//return dp[n, m];
		}

		// for permutation (1..n)
		// 任意の数列に対しては、座標圧縮してから呼び出します。
		public static long InversionNumber(int n, int[] a)
		{
			var r = 0L;
			var bit = new BIT(n);
			for (int i = 0; i < n; ++i)
			{
				r += i - bit.Sum(a[i]);
				bit.Add(a[i], 1);
			}
			return r;
		}
	}

	// 機能限定版
	class BIT
	{
		// Power of 2
		int n2 = 1;
		long[] a;

		public BIT(int n)
		{
			while (n2 < n) n2 <<= 1;
			a = new long[n2 + 1];
		}

		public long this[int i] => Sum(i) - Sum(i - 1);

		public void Add(int i, long v)
		{
			for (; i <= n2; i += i & -i) a[i] += v;
		}

		public long Sum(int r_in)
		{
			var r = 0L;
			for (var i = r_in; i > 0; i -= i & -i) r += a[i];
			return r;
		}
	}
}
