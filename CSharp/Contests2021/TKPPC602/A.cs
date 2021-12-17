using System;

class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		return MPow(2, n - 1);
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
