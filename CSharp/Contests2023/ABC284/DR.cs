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

		// WA
		var (x, y) = Pollard(n);
		var (z, w) = Pollard(x);
		BigInteger[] r;

		if (z == 0)
		{
			(z, w) = Pollard(y);
			r = new[] { x, z, w };
		}
		else
		{
			r = new[] { y, z, w };
		}

		r = r.OrderBy(x => x).ToArray();
		if (r[1] == r[2]) Array.Reverse(r);
		return $"{r[0]} {r[2]}";
	}

	// 素因数分解可能である前提の実装
	public static (BigInteger, BigInteger) Pollard(BigInteger n)
	{
		BigInteger f(BigInteger x) => (x * x + 1) % n;

		for (BigInteger i = 1; i < 20; i++)
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
		return (0, 0);
	}

	static BigInteger Gcd(BigInteger a, BigInteger b) { if (b == 0) return a; for (BigInteger r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
