using System;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long a, long b) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var v = 0L;
		foreach (var (a, b) in ps.OrderBy(_ => _.a))
		{
			if (v + k < a) return v + k;
			k -= a - v;

			v = a;
			k += b;
		}
		return v + k;
	}
}
