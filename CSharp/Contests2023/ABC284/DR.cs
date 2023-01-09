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

		var (x, y) = Pollard(n);
		if (IsPrime((long)x)) (x, y) = (y, x);
		var (z, w) = Pollard(x);

		if (y == z) return $"{y} {w}";
		if (y == w) return $"{y} {z}";
		return $"{z} {y}";
	}

	// 素因数分解可能である前提の実装
	public static (BigInteger, BigInteger) Pollard(BigInteger n)
	{
		if ((n & 1) == 0) return (2, n >> 1);
		BigInteger f(BigInteger x) => (x * x + 1) % n;

		for (BigInteger i = 1; ; i++)
		{
			var x = i;
			var y = f(x);
			var d = (BigInteger)1;
			while (d == 1)
			{
				d = Gcd(BigInteger.Abs(x - y), n);
				x = f(x);
				y = f(f(y));
			}
			if (d == 0 || d == n) continue;
			return (d, n / d);
		}
	}

	static BigInteger Gcd(BigInteger a, BigInteger b) { if (b == 0) return a; for (BigInteger r; (r = a % b) > 0; a = b, b = r) ; return b; }

	// Miller-Rabin primality test
	static long[] MRBases = new[] { 2L, 325, 9375, 28178, 450775, 9780504, 1795265022 };
	public static bool IsPrime(long n)
	{
		if (n <= 1) return false;
		if (n == 2) return true;
		if ((n & 1) == 0) return false;

		var d = n - 1;
		while ((d & 1) == 0) d >>= 1;
		foreach (var a in MRBases)
		{
			if (a % n == 0) return true;
			if (BigInteger.ModPow(a, d, n) == 1) continue;
			var comp = true;
			var p = n - 1;
			while ((p & 1) == 0)
			{
				p >>= 1;
				if (BigInteger.ModPow(a, p, n) == n - 1) { comp = false; break; }
			}
			if (comp) return false;
		}
		return true;
	}
}
