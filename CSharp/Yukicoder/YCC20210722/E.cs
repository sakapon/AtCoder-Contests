using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var r = 0L;
		var p = MPow(2, n - 1);
		var M11_2 = MHalf * 11 % M;

		for (int i = n - 1; i >= 0; i--)
		{
			var d = s[i] - '0';

			r += p * d;
			r %= M;

			p *= M11_2;
			p %= M;
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
}
