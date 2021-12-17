using System;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3L();
		var (p, q, r, s) = Read4L();

		var h = (int)(q - p + 1);
		var w = (int)(s - r + 1);
		var c = NewArray2(h, w, '.');

		for (var (x, y) = (0, b - a + p - r); x < h && y < w; x++, y++)
		{
			if (y < 0) continue;
			c[x][y] = '#';
		}
		for (var (x, y) = (0, b + a - p - r); x < h && y >= 0; x++, y--)
		{
			if (y >= w) continue;
			c[x][y] = '#';
		}

		return string.Join("\n", c.Select(a => new string(a)));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
