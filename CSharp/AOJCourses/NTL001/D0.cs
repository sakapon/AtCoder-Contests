using System;
using System.Collections.Generic;
using System.Linq;

class D0
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(Factorize(n).Distinct().Aggregate(n, (t, p) => t / p * (p - 1)));
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
