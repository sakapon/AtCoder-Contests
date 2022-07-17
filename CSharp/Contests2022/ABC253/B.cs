using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var q = from i in Enumerable.Range(0, h)
				from j in Enumerable.Range(0, w)
				select (i, j);

		var ps = q.Where(p => s[p.i][p.j] == 'o').ToArray();
		return Math.Abs(ps[0].i - ps[1].i) + Math.Abs(ps[0].j - ps[1].j);
	}
}
