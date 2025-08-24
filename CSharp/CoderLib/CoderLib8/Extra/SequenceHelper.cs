using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

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

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/3/DSL_3_D
		public static IEnumerable<int> SlideMin(int[] a, int k)
		{
			var b = new int[a.Length];
			for (int i = 0, l = 0, r = -1; i < a.Length; ++i)
			{
				while (l <= r && a[b[r]] > a[i]) --r;
				b[++r] = i;
				if (b[l] == i - k) ++l;
				if (i >= k - 1) yield return a[b[l]];
			}
		}

		public static IEnumerable<int> SlideMax(int[] a, int k)
		{
			var b = new int[a.Length];
			for (int i = 0, l = 0, r = -1; i < a.Length; ++i)
			{
				while (l <= r && a[b[r]] < a[i]) --r;
				b[++r] = i;
				if (b[l] == i - k) ++l;
				if (i >= k - 1) yield return a[b[l]];
			}
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
		// merge sort を利用します。
		public static long InversionNumber<T>(T[] a, IComparer<T> c = null)
		{
			var n = a.Length;
			var t = new T[n];
			c = c ?? Comparer<T>.Default;
			var r = 0L;

			for (int k = 1; k < n; k <<= 1)
			{
				var ti = 0;
				for (int L = 0; L < n; L += k << 1)
				{
					int R1 = L | k, R2 = R1 + k;
					if (R2 > n) R2 = n;
					int i1 = L, i2 = R1;
					while (ti < R2)
					{
						if (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[i1], a[i2]) <= 0)
						{
							t[ti++] = a[i1++];
						}
						else
						{
							r += R1 - i1;
							t[ti++] = a[i2++];
						}
					}
				}
				Array.Copy(t, a, n);
			}
			return r;
		}

		// for permutation (0, 1, ..., n-1)
		// 値が重複する場合も可能です。
		public static long InversionNumber(int aMax_ex, int[] a)
		{
			var r = 0L;
			var rsq = new RSQ(aMax_ex);
			foreach (var v in a)
			{
				r += rsq[v + 1, aMax_ex];
				rsq.Add(v, 1);
			}
			return r;
		}

		public static long InversionNumber<T>(T[] a)
		{
			// 値が重複しない場合、次の方法で高速化できます。
			//var p = Enumerable.Range(0, a.Length).ToArray();
			//Array.Sort(a, p);
			var p = Enumerable.Range(0, a.Length).OrderBy(i => a[i]).ToArray();
			return InversionNumber(a.Length, p);
		}

		// for permutation (1, 2, ..., n)
		// 任意の数列に対しては、座標圧縮してから呼び出します。
		public static long InversionNumberFrom1(int n, int[] a)
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
