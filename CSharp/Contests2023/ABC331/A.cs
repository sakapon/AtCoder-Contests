using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (M, D) = Read2();
		var (y, m, d) = Read3();

		d++;
		if (d > D) { d = 1; m++; }
		if (m > M) { m = 1; y++; }
		return $"{y} {m} {d}";
	}
}
