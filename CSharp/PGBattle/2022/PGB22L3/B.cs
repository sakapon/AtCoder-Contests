using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (w, h, t) = Read3L();
		var (sx, sy) = Read2L();
		var (tx, ty) = Read2L();

		var w2 = 2 * w;
		var h2 = 2 * h;
		var pd = Enumerable.Range(0, 200010).ToDictionary(i => (long)i * i, i => i);

		var r = 0L;
		Count(tx, ty);
		Count(-tx, ty);
		Count(tx, -ty);
		Count(-tx, -ty);
		return r;

		// (tx-sx+2*w*i)^2 + (ty-sy+2*h*j)^2 = t^2
		void Count(long tx, long ty)
		{
			for (long i = -200000; i <= 200000; i++)
			{
				var di = tx - sx + w2 * i;
				var zz = t * t - di * di;
				if (zz == 0)
				{
					if ((sy - ty) % h2 == 0)
					{
						r++;
					}
				}
				else if (pd.ContainsKey(zz))
				{
					var z = pd[zz];
					if ((sy - ty + z) % h2 == 0)
					{
						r++;
					}
					if ((sy - ty - z) % h2 == 0)
					{
						r++;
					}
				}
			}
		}
	}
}
