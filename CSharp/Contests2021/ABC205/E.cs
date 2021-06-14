using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();

		if (n - m > k) return 0;
		if (n == k) return MNcr(n + m, n);

		return MInt(MNcr(n + m, n) - MNcr(n + m, n - k - 1));
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
