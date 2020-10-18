using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];
		Console.WriteLine(ModNcr(n + k - 1, n, 1000000007));
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
	static long ModFactorial(int n, int mod) { for (long x = 1, i = 1; ; x = x * ++i % mod) if (i >= n) return x; }
	static long ModNpr(int n, int r, int mod)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % mod) if (i >= n) return x;
	}
	static long ModNcr(int n, int r, int p) => n < r ? 0 : n - r < r ? ModNcr(n, n - r, p) : ModNpr(n, r, p) * ModInv((int)ModFactorial(r, p), p) % p;
}
