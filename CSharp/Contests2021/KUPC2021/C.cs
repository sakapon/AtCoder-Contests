using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var max = Math.Max(a.Max(), b.Max());

		var l = new List<(int gacha, int coin)>();

		for (int i = 0; i < n; i++)
		{
			var g = a[i];
			var c = b[i];

			if (c <= g) continue;

			if (l.Count == 0)
			{
				l.Add((g, c));
			}
			else
			{
				var (g0, c0) = l.Last();
				if (c0 < g)
				{
					l.Add((g, c));
				}
				else
				{
					l[l.Count - 1] = (g0, c);
				}
			}
		}

		if (l.Count == 0) return max;

		long r = 2 * max - l[0].gacha;
		var t = r;

		for (int i = 1; i < l.Count; i++)
		{
			var d1 = l[i - 1].coin - l[i - 1].gacha;
			var d2 = l[i].gacha - l[i - 1].coin;

			t += d1;
			t -= d2;
			r = Math.Min(r, t);
		}

		return r;
	}
}
