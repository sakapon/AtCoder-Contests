using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var c = 0;
		var tr = -1 << 30;

		foreach (var (l, r) in ps.OrderBy(p => p.r))
		{
			if (l < tr + d) continue;
			c++;
			tr = r;
		}
		return c;
	}
}
