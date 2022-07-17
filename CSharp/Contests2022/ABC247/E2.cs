using System;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y) = Read3();
		var a = Read();

		var c = 0L;
		var (l, mi, Mi) = (0, -1, -1);

		for (int r = 0; r < n; r++)
		{
			var v = a[r];

			if (v > x || v < y)
			{
				(l, mi, Mi) = (r + 1, -1, -1);
				continue;
			}

			if (v == x) Mi = r;
			if (v == y) mi = r;

			var m = Math.Min(mi, Mi);
			if (m != -1) c += m - l + 1;
		}

		return c;
	}
}
