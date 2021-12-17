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
		var (n, k) = Read2();
		var c = Read();
		var p = ReadL();

		var vs = Enumerable.Range(0, n).GroupBy(i => c[i]).Select(g => g.Min(i => p[i])).ToArray();
		if (vs.Length < k) return -1;

		Array.Sort(vs);
		return vs[..k].Sum();
	}
}
