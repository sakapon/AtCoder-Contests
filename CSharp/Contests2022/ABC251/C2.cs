using System;
using System.Collections.Generic;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new Dictionary<string, (int i, int t)>();
		for (int i = 1; i <= n; i++)
		{
			var p = Console.ReadLine().Split();
			d.TryAdd(p[0], (i, int.Parse(p[1])));
		}
		return d.Values.FirstMax(p => p.t).i;
	}
}

public static class ArgHelper
{
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
}
