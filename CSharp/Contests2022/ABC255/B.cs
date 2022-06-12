using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();
		var ps = Array.ConvertAll(new bool[n], _ => ((long x, long y))Read2());

		var r = ps.Max(p => a.Select(i => ps[i - 1]).Select(q => (x: p.x - q.x, y: p.y - q.y)).Min(d => d.x * d.x + d.y * d.y));
		return Math.Sqrt(r);
	}
}
