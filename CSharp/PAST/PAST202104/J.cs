using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var sum = ps.Sum(p => (double)(c - p.y) * (c - p.y));

		var minX = ArgTrue(-100000, 100000, (i, o) =>
		{
			return ps.Sum(p => (i - p.x) * (i - p.x)) <= ps.Sum(p => (o - p.x) * (o - p.x));
		});

		sum += ps.Sum(p => (minX - p.x) * (minX - p.x));
		return sum;
	}

	// 解の候補が [1, 5] の場合、l = 0, r = 6 を指定します。
	// f: (inside, outside) => isInsideMore
	// 局所最小の例:
	// ArgTrue(0, n + 1, (i, o) => Get(i) <= Get(o))
	static double ArgTrue(double l, double r, Func<double, double, bool> f, int digits = 9)
	{
		var m = l + (r - l) / 2;
		double t;

		while (Math.Round(m - l, digits) > 0 || Math.Round(r - m, digits) > 0)
			if (m - l >= r - m)
			{
				if (f(t = m - (m - l) / 2, m)) (m, r) = (t, m);
				else l = t;
			}
			else
			{
				if (f(t = m + (r - m) / 2, m)) (m, l) = (t, m);
				else r = t;
			}
		return m;
	}
}
