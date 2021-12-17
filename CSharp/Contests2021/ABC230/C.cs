using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3L();
		var (p, q, r, s) = Read4L();

		var c = NewArray2((int)(q - p + 1), (int)(s - r + 1), '.');

		for (var x = p; x <= q; x++)
		{
			var y = x - a + b;
			if (r <= y && y <= s)
			{
				c[x - p][y - r] = '#';
			}

			y = -(x - a) + b;
			if (r <= y && y <= s)
			{
				c[x - p][y - r] = '#';
			}
		}
		return string.Join("\n", c.Select(a => new string(a)));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
