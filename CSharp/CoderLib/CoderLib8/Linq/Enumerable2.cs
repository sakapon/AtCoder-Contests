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
		public static T Max<T>(this IEnumerable<T> source, T iv = default) where T : IComparable<T>
		{
			foreach (var v in source) if (iv.CompareTo(v) < 0) iv = v;
			return iv;
		}
		public static T Min<T>(this IEnumerable<T> source, T iv = default) where T : IComparable<T>
		{
			foreach (var v in source) if (iv.CompareTo(v) > 0) iv = v;
			return iv;
		}
	}
}
