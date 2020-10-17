using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	struct P
	{
		public long v, w;
		public P(long _v, long _w) { v = _v; w = _w; }
	}

	static long[] Read() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		long n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var m1 = Math.Min(20, n);
		var l1 = new List<P> { default(P) };
		for (int i = 0; i < m1; i++)
		{
			var p = ps[i];
			for (int li = l1.Count - 1; li >= 0; li--)
			{
				var p0 = l1[li];
				if (p0.w + p[1] <= w) l1.Add(new P(p0.v + p[0], p0.w + p[1]));
			}
		}

		if (n <= 20)
		{
			Console.WriteLine(l1.Max(P => P.v));
			return;
		}

		var l2 = new List<P> { default(P) };
		for (int i = 20; i < n; i++)
		{
			var p = ps[i];
			for (int li = l2.Count - 1; li >= 0; li--)
			{
				var p0 = l2[li];
				if (p0.w + p[1] <= w) l2.Add(new P(p0.v + p[0], p0.w + p[1]));
			}
		}

		var q1 = l1.GroupBy(p => p.w).OrderBy(g => g.Key).Select(g => new P(g.Max(p => p.v), g.Key));
		var q2 = l2.GroupBy(p => p.w).OrderBy(g => g.Key).Select(g => new P(g.Max(p => p.v), g.Key));

		var lo1 = new List<P>();
		var lo2 = new List<P>();
		var t = -1L;

		foreach (var p in q1)
		{
			if (p.v <= t) continue;
			lo1.Add(p);
			t = p.v;
		}
		t = -1;
		foreach (var p in q2)
		{
			if (p.v <= t) continue;
			lo2.Add(p);
			t = p.v;
		}

		var r = 0L;
		foreach (var p in lo1)
		{
			var li = Last(-1, lo2.Count - 1, x => p.w + lo2[x].w <= w);
			r = Math.Max(r, p.v + lo2[li].v);
		}
		Console.WriteLine(r);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
