using System;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, a, d, n) = Read4L();

		if (d >= 0)
		{
			var min = a;
			var max = a + d * (n - 1);
			if (x <= min) return min - x;
			if (x >= max) return x - max;

			var mod = (x - a) % d;
			return Math.Min(mod, d - mod);
		}
		else
		{
			var min = a + d * (n - 1);
			var max = a;
			if (x <= min) return min - x;
			if (x >= max) return x - max;

			var mod = (a - x) % -d;
			return Math.Min(mod, -d - mod);
		}
	}
}
