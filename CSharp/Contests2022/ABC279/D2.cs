using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();

		var c = First(0, 1L << 60, x => GetTime(x + 1) - GetTime(x) >= 0);
		return GetTime(c);

		double GetTime(long count)
		{
			return b * (double)count + a / Math.Sqrt(1 + count);
		}
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
