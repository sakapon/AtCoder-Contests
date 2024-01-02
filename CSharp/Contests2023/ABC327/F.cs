using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int t, int x) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d, w) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var lu = ps.ToLookup(p => p.t, p => p.x);
		const int max = 200000;

		var r = 0L;
		var st = new LST<long, long>(max,
			(x, y) => x + y, 0,
			Math.Max, int.MinValue,
			(x, p, _, l) => p + x,
			new long[max]);

		for (int t = 0; t <= max; t++)
		{
			foreach (var x in lu[t])
			{
				st.Set(Math.Max(0, x - w), x, 1);
			}

			if (t >= d)
			{
				foreach (var x in lu[t - d])
				{
					st.Set(Math.Max(0, x - w), x, -1);
				}
			}

			Chmax(ref r, st.Get(0, max));
		}
		return r;
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
