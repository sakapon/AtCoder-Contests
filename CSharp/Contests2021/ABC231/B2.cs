using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		return ps.GroupBy(x => x).FirstArgMax(g => g.Count()).Key;
	}
}

public static class ArgHelper
{
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
