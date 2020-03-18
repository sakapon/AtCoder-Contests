using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		Console.WriteLine(Factorize(Gcd(h[0], h[1])).Distinct().Count() + 1);
	}

	static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x)
			while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
