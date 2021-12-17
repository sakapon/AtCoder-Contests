using System;
using System.Collections.Generic;

namespace CoderLib8.Linq
{
	// Test: https://codeforces.com/contest/1490/problem/D
	// Test: https://atcoder.jp/contests/abc231/tasks/abc231_b
	public static class ArgHelper
	{
		public static int ArgMostIndex<TSource, TValue>(this TSource[] a, Func<TSource, TValue> selector, Func<TValue, TValue, bool> isMost, TValue v0)
		{
			var (mi, mv) = (-1, v0);
			for (int i = 0; i < a.Length; ++i)
			{
				var v = selector(a[i]);
				if (isMost(v, mv)) (mi, mv) = (i, v);
			}
			return mi;
		}
		public static int FirstArgMaxIndex<TSource>(this TSource[] a, Func<TSource, int> selector) => ArgMostIndex(a, selector, (x, y) => x > y, int.MinValue);
		public static int FirstArgMinIndex<TSource>(this TSource[] a, Func<TSource, int> selector) => ArgMostIndex(a, selector, (x, y) => x < y, int.MaxValue);

		public static int ArgMostIndex<TSource>(this TSource[] a, Func<TSource, TSource, bool> isMost, TSource v0) => ArgMostIndex(a, x => x, isMost, v0);
		public static int FirstArgMaxIndex(this int[] a) => ArgMostIndex(a, (x, y) => x > y, int.MinValue);
		public static int FirstArgMinIndex(this int[] a) => ArgMostIndex(a, (x, y) => x < y, int.MaxValue);
		public static int LastArgMaxIndex(this int[] a) => ArgMostIndex(a, (x, y) => x >= y, int.MinValue);
		public static int LastArgMinIndex(this int[] a) => ArgMostIndex(a, (x, y) => x <= y, int.MaxValue);

		public static TSource ArgMost<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, Func<TValue, TValue, bool> isMost, TSource o0, TValue v0)
		{
			var (mo, mv) = (o0, v0);
			foreach (var o in source)
			{
				var v = selector(o);
				if (isMost(v, mv)) (mo, mv) = (o, v);
			}
			return mo;
		}
		public static TSource FirstArgMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, TSource o0 = default) => ArgMost(source, selector, (x, y) => x > y, o0, int.MinValue);
		public static TSource FirstArgMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, TSource o0 = default) => ArgMost(source, selector, (x, y) => x < y, o0, int.MaxValue);
	}
}
