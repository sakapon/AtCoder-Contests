using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l, t, x) = Read4();
		var ps = Array.ConvertAll(new bool[n], _ => Read2()).Select(p => (p.a, b: p.b >= l)).ToArray();

		if (ps.Any(p => p.a > t && p.b)) return "forever";

		var r = 0;
		var ta = 0;

		foreach (var (a, b) in ps)
		{
			if (b)
			{
			Start:
				if (ta + a > t)
				{
					r += t - ta;
					r += x;
					ta = 0;
					goto Start;
				}
				else if (ta + a == t)
				{
					r += a;
					r += x;
					ta = 0;
				}
				else
				{
					r += a;
					ta += a;
				}
			}
			else
			{
				r += a;
				ta = 0;
			}
		}

		return r;
	}
}
