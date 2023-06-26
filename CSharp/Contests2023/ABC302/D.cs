using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections.Statics.Typed;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, d) = Read3L();
		var a = ReadL();
		var b = ReadL();

		Array.Sort(a);
		var set = new ArrayItemSet<long>(a, long.MinValue);
		return b
			.Select(x =>
			{
				var sup = set.GetLastLeq(x + d);
				if (sup < x - d) return -1;
				return sup + x;
			})
			.Max(-1);
	}
}

public static class EnumerableHelper
{
	public static long Max(this IEnumerable<long> source, long iv = long.MinValue)
	{
		foreach (var v in source) if (iv < v) iv = v;
		return iv;
	}
}
