using System;

class A
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(MPow(6, n / 2));
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
