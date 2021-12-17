﻿using System;
using System.Linq;

class G2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static (long length, long a0, long d) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => Read3L());

		return Last(0, 1L << 60, x =>
		{
			return ps.Sum(p => x <= p.a0 ? 0 : Math.Min(p.length, (x - p.a0 - 1) / p.d + 1)) < k;
		});
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
