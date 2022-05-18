using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();
		var a = Read();

		var maxes = new List<int>();
		var mins = new List<int>();
		var exts = new List<int>();

		for (int i = 0; i < n; i++)
		{
			var v = a[i];
			if (v == x)
			{
				maxes.Add(i);
			}
			if (v == y)
			{
				mins.Add(i);
			}
			if (v > x || v < y)
			{
				exts.Add(i);
			}
		}

		var r = 0L;

		for (int i = 0; i < n; i++)
		{
			var c = GeqIndex(exts, i) - Math.Max(GeqIndex(maxes, i), GeqIndex(mins, i));
			if (c > 0) r += c;
		}

		return r;

		int GeqIndex(List<int> l, int si)
		{
			var j = First(0, l.Count, x => l[x] >= si);
			return j < l.Count ? l[j] : n;
		}
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
