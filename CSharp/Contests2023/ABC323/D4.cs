using System;
using System.Linq;
using System.Numerics;

class D4
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long s, long c) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		return ps
			.Select(p => (s: p.s / (-p.s & p.s), c: p.c * (-p.s & p.s)))
			.GroupBy(p => p.s)
			.Sum(g => BitOperations.PopCount((ulong)g.Sum(p => p.c)));
	}
}
