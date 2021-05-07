using System;
using System.Collections.Generic;
using System.Linq;

class Q009
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = 0D;

		for (int i = 0; i < n; i++)
		{
			var es = new List<double>();

			var (x0, y0) = ps[i];
			for (int j = 0; j < n; j++)
			{
				if (j == i) continue;
				var (x, y) = ps[j];
				es.Add(Math.Atan2(y - y0, x - x0) * 180 / Math.PI);
			}
			es.Sort();
			var es2 = es.Concat(es.Select(v => v + 360)).ToArray();

			for (int j = 0; j < es.Count; j++)
			{
				var v = es[j] + 180;
				var ei = First(0, es2.Length, x => es2[x] > v);

				r = Math.Max(r, es2[ei - 1] - es[j]);
				r = Math.Max(r, 360 - es2[ei] + es[j]);
			}
		}
		return r;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
