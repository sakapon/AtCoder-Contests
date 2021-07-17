using System;

class D
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		Console.WriteLine(MInt(MPow(2, h[0]) - MNcr(h[0], h[1]) - MNcr(h[0], h[2]) - 1));
	}

	const long M = 1000000007;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	// n >= 0
	static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
	static long MNpr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
	}
	static long MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : MNpr(n, r) * MInv(MFactorial(r)) % M;

	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
