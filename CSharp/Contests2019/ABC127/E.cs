using System;
using System.Numerics;

class E
{
	static void Main()
	{
		var M = 1000000007;
		var a = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long h = a[0], w = a[1], k = a[2];

		Func<BigInteger, BigInteger, BigInteger> c = (x, y) => x * (x * x - 1) / 6 * y * y;
		Console.WriteLine((c(h, w) + c(w, h)) * ModNcr(h * w - 2, k - 2, M) % M);
	}

	static long ModPow(long b, int i, int mod)
	{
		for (var r = 1L; ; b = b * b % mod)
		{
			if (i % 2 > 0) r = r * b % mod;
			if ((i /= 2) < 1) return r;
		}
	}
	static long ModInv(int x, int p) => ModPow(x, p - 2, p);

	static long ModFactorial(long n, int mod) { for (long x = 1, i = 1; ; x = x * ++i % mod) if (i >= n) return x; }
	static long ModNpr(long n, long r, int mod)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % mod) if (i >= n) return x;
	}
	static long ModNcr(long n, long r, int p) => n < r ? 0 : n - r < r ? ModNcr(n, n - r, p) : ModNpr(n, r, p) * ModInv((int)ModFactorial(r, p), p) % p;
}
