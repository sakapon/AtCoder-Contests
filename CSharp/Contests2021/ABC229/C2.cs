using System;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long a, long b) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, w) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		return ps.OrderBy(p => -p.a).Sum(p =>
		{
			var v = Math.Min(p.b, w);
			w -= v;
			return p.a * v;
		});
	}
}
