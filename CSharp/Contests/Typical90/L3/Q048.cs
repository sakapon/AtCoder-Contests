using System;
using System.Linq;

class Q048
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		return ps.Select(p => (long)p.b).Concat(ps.Select(p => (long)p.a - p.b)).OrderBy(x => -x).Take(k).Sum();
	}
}
