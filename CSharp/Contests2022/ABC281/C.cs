using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t) = Read2L();
		var a = ReadL();

		var s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];

		t %= s[^1];

		var ri = Enumerable.Range(0, (int)n + 1).First(i => s[i] > t);
		return $"{ri} {t - s[ri - 1]}";
	}
}
