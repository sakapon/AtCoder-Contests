using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = long.Parse(Console.ReadLine());

		var ps = Factorize(k);
		var r = 0L;

		foreach (var g in ps.GroupBy(p => p))
		{
			var p = g.Key;
			var c = g.Count();

			for (var x = p; ; x += p)
			{
				var y = x;
				while (y % p == 0)
				{
					y /= p;
					if (--c == 0)
					{
						ChMax(ref r, x);
						goto br;
					}
				}
			}
		br:;
		}
		return r;
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}

	public static void ChMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
