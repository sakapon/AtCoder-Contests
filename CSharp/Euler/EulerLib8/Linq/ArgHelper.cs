﻿using System;
using System.Collections.Generic;

namespace EulerLib8.Linq
{
	public static class ArgHelper
	{
		public static T FirstMax<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) < 0 ? o2 : o1;
		public static T FirstMin<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) > 0 ? o2 : o1;
		public static T LastMax<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) <= 0 ? o2 : o1;
		public static T LastMin<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) >= 0 ? o2 : o1;

		public static T FirstMax<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) < 0 ? o2 : o1;
		public static T FirstMin<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) > 0 ? o2 : o1;
		public static T LastMax<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) <= 0 ? o2 : o1;
		public static T LastMin<T, TKey>(T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> => toKey(o1).CompareTo(toKey(o2)) >= 0 ? o2 : o1;

		public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
		public static void ChFirstMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) > 0) o1 = o2; }
		public static void ChLastMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) <= 0) o1 = o2; }
		public static void ChLastMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) >= 0) o1 = o2; }

		public static void ChFirstMax<T, TKey>(ref T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> { if (toKey(o1).CompareTo(toKey(o2)) < 0) o1 = o2; }
		public static void ChFirstMin<T, TKey>(ref T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> { if (toKey(o1).CompareTo(toKey(o2)) > 0) o1 = o2; }
		public static void ChLastMax<T, TKey>(ref T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> { if (toKey(o1).CompareTo(toKey(o2)) <= 0) o1 = o2; }
		public static void ChLastMin<T, TKey>(ref T o1, T o2, Func<T, TKey> toKey) where TKey : IComparable<TKey> { if (toKey(o1).CompareTo(toKey(o2)) >= 0) o1 = o2; }

		public static TSource FirstMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
		{
			var e = source.GetEnumerator();
			if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
			var (mo, mkey) = (e.Current, toKey(e.Current));
			while (e.MoveNext())
			{
				var key = toKey(e.Current);
				if (mkey.CompareTo(key) < 0) (mo, mkey) = (e.Current, key);
			}
			return mo;
		}
		public static TSource FirstMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
		{
			var e = source.GetEnumerator();
			if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
			var (mo, mkey) = (e.Current, toKey(e.Current));
			while (e.MoveNext())
			{
				var key = toKey(e.Current);
				if (mkey.CompareTo(key) > 0) (mo, mkey) = (e.Current, key);
			}
			return mo;
		}
		public static TSource LastMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
		{
			var e = source.GetEnumerator();
			if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
			var (mo, mkey) = (e.Current, toKey(e.Current));
			while (e.MoveNext())
			{
				var key = toKey(e.Current);
				if (mkey.CompareTo(key) <= 0) (mo, mkey) = (e.Current, key);
			}
			return mo;
		}
		public static TSource LastMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
		{
			var e = source.GetEnumerator();
			if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
			var (mo, mkey) = (e.Current, toKey(e.Current));
			while (e.MoveNext())
			{
				var key = toKey(e.Current);
				if (mkey.CompareTo(key) >= 0) (mo, mkey) = (e.Current, key);
			}
			return mo;
		}

		public static TSource FirstMax<TSource>(this IEnumerable<TSource> source, TSource seed) where TSource : IComparable<TSource>
		{
			foreach (var o in source) if (seed.CompareTo(o) < 0) seed = o;
			return seed;
		}
		public static TSource FirstMin<TSource>(this IEnumerable<TSource> source, TSource seed) where TSource : IComparable<TSource>
		{
			foreach (var o in source) if (seed.CompareTo(o) > 0) seed = o;
			return seed;
		}
		public static TSource LastMax<TSource>(this IEnumerable<TSource> source, TSource seed) where TSource : IComparable<TSource>
		{
			foreach (var o in source) if (seed.CompareTo(o) <= 0) seed = o;
			return seed;
		}
		public static TSource LastMin<TSource>(this IEnumerable<TSource> source, TSource seed) where TSource : IComparable<TSource>
		{
			foreach (var o in source) if (seed.CompareTo(o) >= 0) seed = o;
			return seed;
		}

		public static TSource FirstMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey, TSource seed) where TKey : IComparable<TKey>
		{
			TKey mkey = toKey(seed), key;
			foreach (var o in source) if (mkey.CompareTo(key = toKey(o)) < 0) (seed, mkey) = (o, key);
			return seed;
		}
		public static TSource FirstMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey, TSource seed) where TKey : IComparable<TKey>
		{
			TKey mkey = toKey(seed), key;
			foreach (var o in source) if (mkey.CompareTo(key = toKey(o)) > 0) (seed, mkey) = (o, key);
			return seed;
		}
		public static TSource LastMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey, TSource seed) where TKey : IComparable<TKey>
		{
			TKey mkey = toKey(seed), key;
			foreach (var o in source) if (mkey.CompareTo(key = toKey(o)) <= 0) (seed, mkey) = (o, key);
			return seed;
		}
		public static TSource LastMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey, TSource seed) where TKey : IComparable<TKey>
		{
			TKey mkey = toKey(seed), key;
			foreach (var o in source) if (mkey.CompareTo(key = toKey(o)) >= 0) (seed, mkey) = (o, key);
			return seed;
		}
	}
}
