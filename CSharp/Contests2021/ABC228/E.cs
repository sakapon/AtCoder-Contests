using System;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, m) = Read3L();

		k %= M - 1;
		m %= M;
		if (m == 0) return 0;

		var kn = MPow(k, n, M - 1);
		return MPow(m, kn, M);
	}

	const long M = 998244353;
	static long MPow(long b, long i, long mod)
	{
		long r = 1;
		for (; i != 0; b = b * b % mod, i >>= 1) if ((i & 1) != 0) r = r * b % mod;
		return r;
	}
}
