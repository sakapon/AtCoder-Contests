using System;
using System.Collections.Generic;
using System.Linq;

class L2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (k, m) = Read2();
		var ps = Array.ConvertAll(new bool[k], _ => Read2L());

		var r = 0L;
		foreach (var (c, d) in ps)
		{
			var (pow, rep) = Pow10AndRepunit(d);
			r = (r * pow + c * rep) % m;
		}
		return r;

		(long, long) Pow10AndRepunit(long d)
		{
			if (d == 1) return (10 % m, 1);
			var (pow, rep) = Pow10AndRepunit(d >> 1);
			rep = rep * (pow + 1) % m;
			pow = pow * pow % m;
			if ((d & 1) != 0)
			{
				pow = pow * 10 % m;
				rep = (rep * 10 + 1) % m;
			}
			return (pow, rep);
		}
	}
}
