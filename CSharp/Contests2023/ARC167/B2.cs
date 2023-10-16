using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2L();

		var bm = b % M;
		var cs = Factorize(a).GroupBy(p => p).Select(g => g.Count()).ToArray();
		var r = cs.Aggregate(bm, (v, c) => (c * bm + 1) % M * v % M);

		if (b % 2 == 1 && cs.All(c => c % 2 == 0)) r += M - 1;
		return r * MHalf % M;
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
