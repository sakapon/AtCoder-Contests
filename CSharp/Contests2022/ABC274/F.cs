using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int w, int x, int v) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var r = 0;

		// 魚 i を網の左端に固定します。
		for (int i = 0; i < n; i++)
		{
			var l = new List<(double t, int w)>();
			foreach (var (w, x0, v0) in ps)
			{
				var x = x0 - ps[i].x;
				double v = v0 - ps[i].v;

				if (0 <= x && x <= a) l.Add((0, w));

				if (v == 0) continue;
				var t0 = -x / v;
				var ta = (a - x) / v;
				if (t0 > 0 || t0 == 0 && v < 0) l.Add((t0, v > 0 ? w : -w));
				if (ta > 0 || ta == 0 && v > 0) l.Add((ta, v < 0 ? w : -w));
			}

			var tw = 0;
			foreach (var (_, w) in l.OrderBy(p => p.t).ThenBy(p => -p.w))
			{
				tw += w;
				r = Math.Max(r, tw);
			}
		}
		return r;
	}
}
