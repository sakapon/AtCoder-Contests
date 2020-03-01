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
		long rn = (long)Math.Ceiling(Math.Sqrt(n)), x = 2;
		var r = new List<long>();
		while (n % x == 0) { r.Add(x); n /= x; }
		for (x++; x <= rn && n > 1; x += 2)
			while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
