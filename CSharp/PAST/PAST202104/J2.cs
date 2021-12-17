using System;
using System.Linq;

class J2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var x0 = Min(-1 << 20, 1 << 20, x => ps.Sum(p => x - p.x) >= 0);
		return ps.Sum(p => (x0 - p.x) * (x0 - p.x) + (c - p.y) * (c - p.y));
	}

	static double Min(double l, double r, Func<double, bool> f, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
		return r;
	}
}
