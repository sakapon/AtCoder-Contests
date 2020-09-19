using System;

namespace CoderLib8.Collections
{
	static class SortHelper
	{
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
	}
}
