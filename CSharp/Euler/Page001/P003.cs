using System;
using System.Collections.Generic;
using System.Linq;

class P003
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const long n = 600851475143;
		return Factorize(n).Last();
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
