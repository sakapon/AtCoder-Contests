using System;
using System.Collections.Generic;

class B
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine()) * 2;

		var m = n - 1;
		foreach (var d1 in Divisors(n))
		{
			var d2 = n / d1;
			if (Gcd(d1, d2) != 1) continue;
			var x = Crt(d1, d2, 0, -1);
			m = Math.Min(m, x == 0 ? n : x);
		}
		Console.WriteLine(m);
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

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

	// a mod m かつ b mod n である値 (mod mn で唯一)
	// 前提: m と n は互いに素。
	static long Crt(long m, long n, long a, long b)
	{
		var v = ExtendedEuclid(m, n);
		var r = a * n * v[1] + b * m * v[0];
		return (r %= m * n) < 0 ? r + m * n : r;
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
