using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var ps = Enumerable.Range(0, n - 1)
			.Select(i => s[i..(i + 2)])
			.GroupBy(t => t)
			.Select(g => (t: g.Key, c: g.Count()))
			.ToArray();
		var max = ps.Max(p => p.c);
		return ps.Where(p => p.c == max).Min(p => p.t);
	}
}
