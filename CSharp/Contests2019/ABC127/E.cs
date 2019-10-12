using System;
using System.Numerics;

class E
{
	static void Main()
	{
		var a = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		BigInteger h = a[0], w = a[1], k = a[2];

		Func<BigInteger, BigInteger, BigInteger> c = (x, y) => x * (x * x - 1) / 6 * y * y;
		Console.WriteLine((c(h, w) + c(w, h)) * Ncr(h * w - 2, k - 2) % 1000000007);
	}

	static BigInteger Factorial(BigInteger n) { for (BigInteger x = 1, i = 1; ; x *= ++i) if (i >= n) return x; }
	static BigInteger Npr(BigInteger n, BigInteger r)
	{
		if (n < r) return 0;
		for (BigInteger x = 1, i = n - r; ; x *= ++i) if (i >= n) return x;
	}
	static BigInteger Ncr(BigInteger n, BigInteger r) => n < r ? 0 : n - r < r ? Ncr(n, n - r) : Npr(n, r) / Factorial(r);
}
