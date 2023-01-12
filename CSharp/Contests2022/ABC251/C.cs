using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		return ps
			.Select((p, i) => (s: p[0], t: int.Parse(p[1]), i))
			.GroupBy(p => p.s)
			.Select(g => g.First())
			.OrderByDescending(p => p.t)
			.First().i + 1;
	}
}
