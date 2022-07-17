using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	static class SortHelper
	{
		#region For String
		// Test: https://codeforces.com/contest/1450/problem/A

		static int[] Tally(string s)
		{
			var c = new int[1 << 7];
			foreach (var x in s) ++c[x];
			return c;
		}

		static char[] BucketSort(string s)
		{
			var c = Tally(s);
			for (int i = 1; i < c.Length; ++i) c[i] += c[i - 1];
			var r = new char[s.Length];
			for (int i = s.Length - 1; i >= 0; --i) r[--c[s[i]]] = s[i];
			return r;
		}
		#endregion

		static int[] Tally(int[] a, int max)
		{
			var c = new int[max + 1];
			foreach (var x in a) ++c[x];
			return c;
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/6/ALDS1_6_A
		static int[] BucketSort(int[] a, int max)
		{
			var s = Tally(a, max);
			for (int i = 0; i < max; ++i) s[i + 1] += s[i];
			var r = new int[a.Length];
			for (int i = a.Length - 1; i >= 0; --i) r[--s[a[i]]] = a[i];
			return r;
		}

		static T[] BucketSort<T>(T[] a, Func<T, int> toKey, int max)
		{
			var keys = Array.ConvertAll(a, x => toKey(x));
			var s = Tally(keys, max);
			for (int i = 0; i < max; ++i) s[i + 1] += s[i];
			var r = new T[a.Length];
			for (int i = a.Length - 1; i >= 0; --i) r[--s[keys[i]]] = a[i];
			return r;
		}

		// Int32 のすべての値が対象です。
		static void RadixSort(int[] a)
		{
			var f = 0xFF;
			BucketSort(a, x => x & f, f);
			BucketSort(a, x => x >> 8 & f, f);
			BucketSort(a, x => x >> 16 & f, f);
			BucketSort(a, x => x >> 24 & f ^ 0x80, f);
		}

		// Int64 のすべての値が対象です。
		static void RadixSort(long[] a)
		{
			var f = 0xFF;
			BucketSort(a, x => (int)x & f, f);
			for (int b = 8; b < 56; b += 8)
				BucketSort(a, x => (int)(x >> b) & f, f);
			BucketSort(a, x => (int)(x >> 56) & f ^ 0x80, f);
		}

		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
		static void MergeSort(int[] a)
		{
			var n = a.Length;
			var t = new int[n];

			for (int k = 1; k < n; k <<= 1)
			{
				var ti = 0;
				for (int L = 0; L < n; L += k << 1)
				{
					int R1 = L | k, R2 = R1 + k;
					if (R2 > n) R2 = n;
					int i1 = L, i2 = R1;
					while (ti < R2) t[ti++] = (i2 >= R2 || i1 < R1 && i2 < R2 && a[i1] <= a[i2]) ? a[i1++] : a[i2++];
				}
				Array.Copy(t, a, n);
			}
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_A
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_B
		// Test: https://atcoder.jp/contests/abc256/tasks/abc256_d
		static void MergeSort<T>(T[] a, IComparer<T> c = null)
		{
			var n = a.Length;
			var t = new T[n];
			c = c ?? Comparer<T>.Default;

			for (int k = 1; k < n; k <<= 1)
			{
				var ti = 0;
				for (int L = 0; L < n; L += k << 1)
				{
					int R1 = L | k, R2 = R1 + k;
					if (R2 > n) R2 = n;
					int i1 = L, i2 = R1;
					while (ti < R2) t[ti++] = (i2 >= R2 || i1 < R1 && i2 < R2 && c.Compare(a[i1], a[i2]) <= 0) ? a[i1++] : a[i2++];
				}
				Array.Copy(t, a, n);
			}
		}

		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
		// in-place
		static void HeapSort(int[] a)
		{
			int i = 0, c, p;
			while (++i < a.Length)
			{
				for (c = i; c > 0 && a[c] > a[p = (c - 1) >> 1]; c = p)
				{
					(a[c], a[p]) = (a[p], a[c]);
				}
			}
			while (--i > 0)
			{
				(a[0], a[i]) = (a[i], a[0]);
				for (p = 0; (c = p << 1 | 1) < i; p = c)
				{
					if (c + 1 < i && a[c] < a[c + 1]) ++c;
					if (a[c] <= a[p]) break;
					(a[c], a[p]) = (a[p], a[c]);
				}
			}
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_A
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_B
		// Test: https://atcoder.jp/contests/abc256/tasks/abc256_d
		// in-place
		static void HeapSort<T>(T[] a, IComparer<T> comp = null)
		{
			comp = comp ?? Comparer<T>.Default;
			int i = 0, c, p;
			while (++i < a.Length)
			{
				for (c = i; c > 0 && comp.Compare(a[c], a[p = (c - 1) >> 1]) > 0; c = p)
				{
					(a[c], a[p]) = (a[p], a[c]);
				}
			}
			while (--i > 0)
			{
				(a[0], a[i]) = (a[i], a[0]);
				for (p = 0; (c = p << 1 | 1) < i; p = c)
				{
					if (c + 1 < i && comp.Compare(a[c], a[c + 1]) < 0) ++c;
					if (comp.Compare(a[c], a[p]) <= 0) break;
					(a[c], a[p]) = (a[p], a[c]);
				}
			}
		}

		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_A
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/5/ITP2_5_B
		static void Sort<T, TKey>(T[] a, params Func<T, TKey>[] toKeys)
		{
			var ec = EqualityComparer<TKey>.Default;
			var keys = new TKey[a.Length];

			Action<int, int, int> Dfs = null;
			Dfs = (depth, start, end) =>
			{
				for (int i = start; i < end; ++i) keys[i] = toKeys[depth](a[i]);
				Array.Sort(keys, a, start, end - start);

				if (++depth == toKeys.Length) return;
				for (int s = start, i = start + 1; i <= end; ++i)
				{
					if (i < end && ec.Equals(keys[i - 1], keys[i])) continue;
					if (s != i - 1) Dfs(depth, s, i);
					s = i;
				}
			};
			Dfs(0, 0, a.Length);
		}
	}
}
