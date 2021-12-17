using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int c) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var r = 0L;

		foreach (var (a, c) in ps.OrderBy(p => p.c))
		{
			var g = Gcd(n, a);
			r += (n - g) * (long)c;
			n = g;

			if (n == 1) return r;
		}
		return -1;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
