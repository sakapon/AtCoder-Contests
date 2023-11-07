using System;
using System.Collections.Generic;
using Int = System.Int64;

namespace CoderLib8.Linq
{
	public static class Array2
	{
		#region Initialization
		public static T[] Repeat<T>(T value, int count)
		{
			var a = new T[count];
			Array.Fill(a, value);
			return a;
		}

		public static int[] Range(int start, int count)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i) a[i] = start++;
			return a;
		}
		public static int[] RangeLR(int l, int r) => Range(l, r - l);
		#endregion

		#region Accumulation
		public static TResult Aggregate<TSource, TResult>(this TSource[] a, TResult iv, Func<TResult, TSource, TResult> func)
		{
			foreach (var v in a) iv = func(iv, v);
			return iv;
		}

		public static bool All<TSource>(this TSource[] a, Func<TSource, bool> predicate) => a.Aggregate(true, (r, v) => r && predicate(v));
		public static bool Any<TSource>(this TSource[] a, Func<TSource, bool> predicate) => a.Aggregate(false, (r, v) => r || predicate(v));
		public static long Sum(this long[] a) => a.Aggregate(0L, (r, v) => r + v);
		public static long Max(this long[] a) => a.Aggregate(long.MinValue, Math.Max);
		public static long Min(this long[] a) => a.Aggregate(long.MaxValue, Math.Min);
		#endregion

		public static (int index, Int value) FirstMax(this Int[] a, Int iv = Int.MinValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
				if (iv <= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Int value) FirstMin(this Int[] a, Int iv = Int.MaxValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
				if (iv >= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Int value) LastMax(this Int[] a, Int iv = Int.MinValue)
		{
			var mi = -1;
			for (var i = 0; i < a.Length; ++i)
				if (iv <= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Int value) LastMin(this Int[] a, Int iv = Int.MaxValue)
		{
			var mi = -1;
			for (var i = 0; i < a.Length; ++i)
				if (iv >= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}

		public static (int index, Int value) FirstMax<TSource>(this TSource[] a, Func<TSource, Int> selector, Int iv = Int.MinValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
			{
				var v = selector(a[i]);
				if (iv <= v) (mi, iv) = (i, v);
			}
			return (mi, iv);
		}
		public static (int index, Int value) FirstMin<TSource>(this TSource[] a, Func<TSource, Int> selector, Int iv = Int.MaxValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
			{
				var v = selector(a[i]);
				if (iv >= v) (mi, iv) = (i, v);
			}
			return (mi, iv);
		}
	}
}
