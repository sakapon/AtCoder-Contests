using System;
using System.Linq;

class F3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;

		var r = 0L;
		var p2 = MPow(2, n - 1);
		var d = p2;
		var p10 = 1L;

		for (int i = n - 1; i >= 0; i--)
		{
			r += s[i] * d;
			r %= M;

			p2 = p2 * MHalf % M;

			d -= p2 * p10;
			p10 = p10 * 10 % M;
			d += p2 * p10;
			d = MInt(d);
		}
		return r;
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
