using System;
using System.Linq;

class F4
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;
		if (n == 1) return s[0];

		var p2 = MPow(2, n - 2);
		var d = p2 * 2 % M;
		var r = s[^1] * d % M;

		for (int i = n - 2; i >= 0; i--)
		{
			d = (d * 5 + p2) % M;
			r += s[i] * d;
			r %= M;
		}
		return r;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
