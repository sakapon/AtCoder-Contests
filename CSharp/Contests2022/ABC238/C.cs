using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var s = n % M * ((n + 1) % M) % M * MHalf % M;
		var r = 0L;

		for (long d = 10; ; d *= 10)
		{
			if (d > n)
			{
				var c = n - d / 10 + 1;
				r += c % M * ((d / 10 - 1) % M) % M;
				r %= M;
				break;
			}
			else
			{
				var c = d - d / 10;
				r += c % M * ((d / 10 - 1) % M) % M;
				r %= M;
			}
		}
		return (s - r + M) % M;
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;
}
