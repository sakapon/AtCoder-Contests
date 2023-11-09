using System;
using Num = System.Int64;

namespace CoderLib8.Linq
{
	public static class ArrayArg
	{
		public static (int index, Num value) FirstMax(this Num[] a, Num iv = Num.MinValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
				if (iv <= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Num value) FirstMin(this Num[] a, Num iv = Num.MaxValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
				if (iv >= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Num value) LastMax(this Num[] a, Num iv = Num.MinValue)
		{
			var mi = -1;
			for (var i = 0; i < a.Length; ++i)
				if (iv <= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}
		public static (int index, Num value) LastMin(this Num[] a, Num iv = Num.MaxValue)
		{
			var mi = -1;
			for (var i = 0; i < a.Length; ++i)
				if (iv >= a[i]) (mi, iv) = (i, a[i]);
			return (mi, iv);
		}

		public static (int index, Num value) FirstMax<TSource>(this TSource[] a, Func<TSource, Num> selector, Num iv = Num.MinValue)
		{
			var mi = -1;
			for (var i = a.Length - 1; i >= 0; --i)
			{
				var v = selector(a[i]);
				if (iv <= v) (mi, iv) = (i, v);
			}
			return (mi, iv);
		}
		public static (int index, Num value) FirstMin<TSource>(this TSource[] a, Func<TSource, Num> selector, Num iv = Num.MaxValue)
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
