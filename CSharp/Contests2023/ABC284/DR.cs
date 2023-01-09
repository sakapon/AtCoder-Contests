using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class DR
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var r = Factorize(n);
		return r[0] == r[1] ? $"{r[0]} {r[2]}" : $"{r[2]} {r[0]}";
	}

	// Pollard's rho algorithm
	public static long[] Factorize(long n)
	{
		var ps = new List<long>();
		while ((n & 1) == 0) { ps.Add(2); n >>= 1; }

		var q = new Stack<long>();
		if (n > 1) q.Push(n);

		while (q.Count > 0)
		{
			var x = q.Pop();
			if (IsPrime(x))
			{
				ps.Add(x);
			}
			else
			{
				var d = Pollard(x);
				q.Push(d);
				q.Push(x / d);
			}
		}

		var r = ps.ToArray();
		Array.Sort(r);
		return r;
	}

	// n: 奇数かつ合成数
	static long Pollard(long n)
	{
		long f(BigInteger x) => (long)((x * x + 1) % n);

		for (long i = 1; ; ++i)
		{
			long x = i, y = f(x), d = 1L;
			while (d == 1)
			{
				d = Gcd(Math.Abs(x - y), n);
				x = f(x);
				y = f(f(y));
			}
			if (d == 0 || d == n) continue;
			return d;
		}
	}

	static long Gcd(long a, long b) { if (b == 0) return a; for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

	// Miller-Rabin primality test
	static readonly long[] MRBases = new[] { 2L, 325, 9375, 28178, 450775, 9780504, 1795265022 };
	public static bool IsPrime(long n)
	{
		if (n <= 1) return false;
		if (n == 2) return true;
		if ((n & 1) == 0) return false;

		var s = 0;
		var d = n - 1;
		while ((d & 1) == 0) { ++s; d >>= 1; }

		foreach (var a in MRBases)
		{
			if (a % n == 0) return true;
			var x = BigInteger.ModPow(a, d, n);
			if (x == 1) continue;
			var r = 0;
			for (; r < s; ++r, x = x * x % n) if (x == n - 1) break;
			if (r == s) return false;
		}
		return true;
	}
}
