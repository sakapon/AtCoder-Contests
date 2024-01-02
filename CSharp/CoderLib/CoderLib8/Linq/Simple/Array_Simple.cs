using System;

namespace CoderLib8.Linq.Simple
{
	public static class Array_Simple
	{
		#region Accumulation
		public static TResult Aggregate<TSource, TResult>(this TSource[] a, TResult iv, Func<TResult, TSource, TResult> func)
		{
			foreach (var v in a) iv = func(iv, v);
			return iv;
		}

		public static bool All(this bool[] a) => a.Aggregate(true, (r, v) => r && v);
		public static bool Any(this bool[] a) => a.Aggregate(false, (r, v) => r || v);
		public static long Max(this long[] a) => a.Aggregate(long.MinValue, Math.Max);
		public static long Min(this long[] a) => a.Aggregate(long.MaxValue, Math.Min);
		public static long Sum(this long[] a) => a.Aggregate(0L, (r, v) => r + v);
		public static double Prod(this double[] a) => a.Aggregate(0D, (r, v) => r * v);

		public static bool All<TSource>(this TSource[] a, Func<TSource, bool> predicate) => a.Aggregate(true, (r, v) => r && predicate(v));
		public static bool Any<TSource>(this TSource[] a, Func<TSource, bool> predicate) => a.Aggregate(false, (r, v) => r || predicate(v));
		public static long Max<TSource>(this TSource[] a, Func<TSource, long> selector) => a.Aggregate(0L, (r, v) => Math.Max(r, selector(v)));
		public static long Min<TSource>(this TSource[] a, Func<TSource, long> selector) => a.Aggregate(0L, (r, v) => Math.Min(r, selector(v)));
		public static long Sum<TSource>(this TSource[] a, Func<TSource, long> selector) => a.Aggregate(0L, (r, v) => r + selector(v));
		public static double Prod<TSource>(this TSource[] a, Func<TSource, double> selector) => a.Aggregate(0D, (r, v) => r * selector(v));
		#endregion
	}
}
