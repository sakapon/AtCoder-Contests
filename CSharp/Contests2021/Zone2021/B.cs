using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int d, int h) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, D, H) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		return Math.Max(0, ps.Max(p => H - (double)D * (H - p.h) / (D - p.d)));
	}
}
