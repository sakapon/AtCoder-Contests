using System;
using System.Linq;
using static System.Math;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var h = Read();
		var (ax, ay) = (h[0], h[1]);
		var (bx, by) = (h[2], h[3]);
		var (cx, cy) = (h[4], h[5]);

		var x = ax == bx || ax == cx ? ax : bx;
		var y = ay == by || ay == cy ? ay : by;

		var dx = ax + bx + cx - 3 * x;
		var dy = ay + by + cy - 3 * y;

		var r = -1;
		switch ((dx, dy))
		{
			case (1, 1):
				r = Max(Abs(2 * x), Abs(2 * y));
				if (x == y && (x, y) != (0, 0)) r++;
				break;
			case (-1, 1):
				r = Max(Abs(2 * x - 1), Abs(2 * y));
				break;
			case (-1, -1):
				r = Max(Abs(2 * x - 1), Abs(2 * y - 1));
				if (x == y && (x, y) != (1, 1)) r++;
				break;
			case (1, -1):
				r = Max(Abs(2 * x), Abs(2 * y - 1));
				break;
			default:
				break;
		}
		return r;
	}
}
