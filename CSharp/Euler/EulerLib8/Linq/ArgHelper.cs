using System;
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
			var mo = e.Current;
			while (e.MoveNext())
			{
				var o = e.Current;
				if (toKey(mo).CompareTo(toKey(o)) < 0) mo = o;
			}
			return mo;
		}
		public static TSource FirstMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey) where TKey : IComparable<TKey>
		{
			var e = source.GetEnumerator();
			if (!e.MoveNext()) throw new ArgumentException("The source is empty.", nameof(source));
			var mo = e.Current;
			while (e.MoveNext())
			{
				var o = e.Current;
				if (toKey(mo).CompareTo(toKey(o)) > 0) mo = o;
			}
			return mo;
		}
	}
}
