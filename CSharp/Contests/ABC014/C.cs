using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var raq = StaticRAQ1(1000002, Array.ConvertAll(ps, p => (p[0], p[1] + 1, 1L)));
		return raq.Max();
	}

	static long[] StaticRAQ1(int n, (int l, int r, long v)[] a)
	{
		var d = new long[n];
		foreach (var (l, r, v) in a)
		{
			d[l] += v;
			d[r] -= v;
		}
		for (int i = 1; i < n; ++i) d[i] += d[i - 1];
		return d;
	}
}
