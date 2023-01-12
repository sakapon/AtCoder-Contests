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

		return Enumerable.Range(0, n - 1)
			.Select(i => s[i..(i + 2)])
			.GroupBy(t => t)
			.OrderBy(g => -g.Count())
			.ThenBy(g => g.Key)
			.First().Key;
	}
}
