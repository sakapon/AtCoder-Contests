using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c == '#').ToArray();

		var ds = Divisors(n);

		// 約数 d で初めて現れるシフト表の数
		var cs = new long[n + 1];

		foreach (var d in ds)
		{
			if (d == n) continue;

			var and = new bool[d];
			Array.Fill(and, true);
			for (int i = 0; i < n; i++)
			{
				and[i % d] &= s[i];
			}

			cs[d] = MPow(2, and.Count(b => b));

			foreach (var d2 in ds)
			{
				if (d <= d2) break;
				if (d % d2 == 0) cs[d] -= cs[d2];
			}

			cs[d] = MInt(cs[d]);
		}

		return cs.Sum() % M;
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}

	static long[] Divisors(long n)
	{
		var r = new List<long>();
		for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
		var i = r.Count - 1;
		if (r[i] * r[i] == n) --i;
		for (; i >= 0; --i) r.Add(n / r[i]);
		return r.ToArray();
	}
}
