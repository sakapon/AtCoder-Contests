using System;
using System.Collections.Generic;

namespace CoderLib8.Linq
{
	public static class Enumerable2
	{
		public static int Max(this IEnumerable<int> source, int iv = int.MinValue)
		{
			foreach (var v in source) if (iv < v) iv = v;
			return iv;
		}
		public static int Min(this IEnumerable<int> source, int iv = int.MaxValue)
		{
			foreach (var v in source) if (iv > v) iv = v;
			return iv;
		}
		public static long Max(this IEnumerable<long> source, long iv = long.MinValue)
		{
			foreach (var v in source) if (iv < v) iv = v;
			return iv;
		}
		public static long Min(this IEnumerable<long> source, long iv = long.MaxValue)
		{
			foreach (var v in source) if (iv > v) iv = v;
			return iv;
		}
		public static T Max<T>(this IEnumerable<T> source, T iv) where T : IComparable<T>
		{
			foreach (var v in source) if (iv.CompareTo(v) < 0) iv = v;
			return iv;
		}
		public static T Min<T>(this IEnumerable<T> source, T iv) where T : IComparable<T>
		{
			foreach (var v in source) if (iv.CompareTo(v) > 0) iv = v;
			return iv;
		}
		public static T Max<T>(this IEnumerable<T> source, T iv, IComparer<T> c = null)
		{
			c ??= Comparer<T>.Default;
			foreach (var v in source) if (c.Compare(iv, v) < 0) iv = v;
			return iv;
		}
		public static T Min<T>(this IEnumerable<T> source, T iv, IComparer<T> c = null)
		{
			c ??= Comparer<T>.Default;
			foreach (var v in source) if (c.Compare(iv, v) > 0) iv = v;
			return iv;
		}

		public static (T v, TKey max) MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> toKey, TKey ik, IComparer<TKey> c = null)
		{
			c ??= Comparer<TKey>.Default;
			T v0 = default;
			foreach (var v in source)
			{
				var k = toKey(v);
				if (c.Compare(ik, k) < 0) (v0, ik) = (v, k);
			}
			return (v0, ik);
		}
		public static (T v, TKey min) MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> toKey, TKey ik, IComparer<TKey> c = null)
		{
			c ??= Comparer<TKey>.Default;
			T v0 = default;
			foreach (var v in source)
			{
				var k = toKey(v);
				if (c.Compare(ik, k) > 0) (v0, ik) = (v, k);
			}
			return (v0, ik);
		}
	}
}
