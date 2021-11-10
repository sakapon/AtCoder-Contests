using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var set = new HashSet<(int, int)>();

		for (int i = 0; i < n; i++)
		{
			var (x1, y1) = ps[i];

			for (int j = i + 1; j < n; j++)
			{
				var (x2, y2) = ps[j];

				var (d, e) = (x2 - x1, y2 - y1);

				if (d == 0)
				{
					set.Add((0, 1));
					set.Add((0, -1));
				}
				else if (e == 0)
				{
					set.Add((1, 0));
					set.Add((-1, 0));
				}
				else
				{
					var g = Gcd(Math.Abs(d), Math.Abs(e));
					d /= g;
					e /= g;
					set.Add((d, e));
					set.Add((-d, -e));
				}
			}
		}

		return set.Count;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
