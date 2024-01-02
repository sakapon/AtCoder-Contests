using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b_) = Read2L();
		BigInteger b = b_;

		var cs = Factorize(a).GroupBy(p => p).Select(g => g.Count()).ToArray();
		var r = cs.Aggregate(b, (v, c) => (c * b + 1) * v);
		return r / 2 % M;
	}

	const long M = 998244353;

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
