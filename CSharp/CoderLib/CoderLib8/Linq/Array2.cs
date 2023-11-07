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

		#region Array
		public static T[] ToArray<T>(this T[] a) => (T[])a.Clone();
		public static void Clear<T>(this T[] a) => Array.Clear(a, 0, a.Length);
		public static void Fill<T>(this T[] a, T value) => Array.Fill(a, value);
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

		public static TSource[][] Chunk<TSource>(this TSource[] a, int size)
		{
			var q = Math.DivRem(a.Length, size, out var rem);
			var r = new TSource[rem == 0 ? q : q + 1][];
			for (var i = 0; i < q; ++i)
				r[i] = a[(size * i)..(size * (i + 1))];
			if (rem != 0) r[q] = a[(size * q)..];
			return r;
		}

		public static (TKey key, TSource[] chunk)[] Chunk<TSource, TKey>(this TSource[] a, Func<TSource, TKey> toKey)
		{
			var l = new List<(TKey, TSource[])>();
			TKey key = default;
			var si = 0;
			for (var i = 0; i < a.Length; ++i)
			{
				var k = toKey(a[i]);
				if (!Equals(k, key))
				{
					if (i != 0) l.Add((key, a[si..i]));
					key = k;
					si = i;
				}
			}
			if (a.Length != 0) l.Add((key, a[si..]));
			return l.ToArray();
		}
	}
}
