using System;
using System.Collections.Generic;

namespace EulerLib8.Linq
{
	public static class ArgHelper
	{
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
