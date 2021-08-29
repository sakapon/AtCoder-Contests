using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		(int n, long k) = Read2();
		var a = ReadL();

		var r = 0L;
		var q = new Queue<(long v, int c)>(a.GroupBy(v => v).Select(g => (v: g.Key, g.Count())).OrderBy(_ => -_.v));

		long Play(long v, long nv, long c)
		{
			var all = (v - nv) * c;
			if (all <= k)
			{
				k -= all;
				return (v * (v + 1) / 2 - nv * (nv + 1) / 2) * c;
			}
			else
			{
				var r = 0L;

				var vc = k / c;
				r += v * (v + 1) / 2 * c;
				r -= (v - vc) * (v - vc + 1) / 2 * c;
				k -= vc * c;
				v -= vc;

				r += v * k;
				k = 0;

				return r;
			}
		}

		var nc = 0;

		while (q.Count > 0)
		{
			var (v, c) = q.Dequeue();
			nc += c;

			r += Play(v, q.Count == 0 ? 0 : q.Peek().v, nc);
			if (k == 0) break;
		}

		return r;
	}
}
