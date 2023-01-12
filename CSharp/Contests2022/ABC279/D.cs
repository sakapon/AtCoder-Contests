using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();

		var c = ArgTrue(-1, (long)(GetTime(0) / b + 10), (i, o) => GetTime(i) <= GetTime(o));
		return GetTime(c);

		decimal GetTime(long count)
		{
			return (decimal)b * count + a / (decimal)Math.Sqrt(1 + count);
		}
	}

	static long ArgTrue(long l, long r, Func<long, long, bool> f)
	{
		var m = l + (r - l) / 2;
		long t;

		while (m - l > 1 || r - m > 1)
			if (m - l >= r - m)
			{
				if (f(t = m - (m - l) / 2, m)) (m, r) = (t, m);
				else l = t;
			}
			else
			{
				if (f(t = m + (r - m) / 2, m)) (m, l) = (t, m);
				else r = t;
			}
		return m;
	}
}
