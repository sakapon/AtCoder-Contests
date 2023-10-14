using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d, p) = Read3();
		var f0 = ReadL();

		var f = new long[(n + d - 1) / d * d];
		Array.Copy(f0, 0, f, 0, n);
		Array.Sort(f);
		return Enumerable.Range(0, f.Length).GroupBy(i => i / d).Sum(g => Math.Min(g.Sum(i => f[i]), p));
	}
}
