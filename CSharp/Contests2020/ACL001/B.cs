using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine()) * 2;

		var m = long.MaxValue;
		foreach (var d1 in Divisors(n))
		{
			var d2 = n / d1;
			if (Gcd(d1, d2) != 1) continue;
			m = Math.Min(m, Crt(d1, d2, 0, -1));
		}
		Console.WriteLine(m);
	}

	static long MInt(long x, long M) => (x %= M) < 0 ? x + M : x;
	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

	// 前提: m と n は互いに素。
	static long Crt(long m, long n, long a, long b)
	{
		var e = ExtendedEuclid(m, n);
		var x = a * n * e[1] + b * m * e[0];
		x = MInt(x, m * n);
		return x == 0 ? m * n : x;
	}

	// ax + by = 1 の解 (x, y)
	// 前提: a と b は互いに素。
	// ax + by = GCD(a, b) の解を求める場合、予め GCD(a, b) で割ってからこの関数を利用します。
	static long[] ExtendedEuclid(long a, long b)
	{
		if (b == 1) return new[] { 1, 1 - a };
		long r;
		var q = Math.DivRem(a, b, out r);
		var t = ExtendedEuclid(b, r);
		return new[] { t[1], t[0] - q * t[1] };
	}

	// すべての約数 O(√n)
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
