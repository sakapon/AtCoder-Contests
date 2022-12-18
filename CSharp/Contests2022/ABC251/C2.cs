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
		return d.Values.FirstMax(p => p.t, default).i;
	}
}

public static class ArgHelper
{
	public static TSource FirstMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> toKey, TSource seed) where TKey : IComparable<TKey>
	{
		TKey mkey = toKey(seed), key;
		foreach (var o in source) if (mkey.CompareTo(key = toKey(o)) < 0) (seed, mkey) = (o, key);
		return seed;
	}
}
