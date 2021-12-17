using System;
using System.Linq;

class J3
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var x0 = ps.Average(p => p.x);
		return ps.Sum(p => (x0 - p.x) * (x0 - p.x) + (c - p.y) * (c - p.y));
	}
}
