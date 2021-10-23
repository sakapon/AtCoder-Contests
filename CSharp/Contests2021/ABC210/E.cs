using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long a, long c) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = ((long, int))Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read2L());

		ps = ps.OrderBy(p => p.c).ThenBy(p => p.a).ToArray();

		var r = 0L;
		var p = 1L;

		foreach (var (a, c) in ps)
		{
			var d0 = Gcd(p, a);
			var d = Gcd(n, a / d0);

			r += (n / d - 1) * d * p * c;
			n = d;
			p *= n / d;

			if (n == 1) return r;
		}
		return -1;
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
