using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		// k 層目
		var k = First(1, 1 << 30, x => n <= x * x);
		n -= (k - 1) * (k - 1);
		return n < k ? k - n + 1 : n - k + 1;
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
