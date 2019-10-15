using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		var g = Gcd(h[0], h[1]);

		var l = new List<long>();
		foreach (var d in Divisors(g).Skip(1)) if (l.TrueForAll(x => d % x != 0)) l.Add(d);
		Console.WriteLine(l.Count + 1);
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }

	static long[] Divisors(long v)
	{
		var d = new List<long>();
		var c = 0;
		for (long i = 1, j, rv = (long)Math.Sqrt(v); i <= rv; i++)
			if (v % i == 0)
			{
				d.Insert(c, i);
				if ((j = v / i) != i) d.Insert(++c, j);
			}
		return d.ToArray();
	}
}
