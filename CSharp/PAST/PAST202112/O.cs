using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Numerics;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var b = Read();
		var s = ReadL();

		const int bmax = 100000;
		var c1 = new long[bmax + 1];
		foreach (var x in b) c1[x]++;
		var c2 = (long[])c1.Clone();
		Array.Reverse(c2);

		var c = FNTT.Convolution(c1, c2);
		var r = Enumerable.Range(1, n).Sum(i => (n - i) * c[bmax + i] % M * s[i - 1] % M);
		r %= M;

		for (int i = n - 2; i > 0; i--)
		{
			r *= i;
			r %= M;
		}
		return r;
	}

	const long M = 998244353;
}
