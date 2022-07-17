using System;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, a, d, n) = Read4L();
		if (d < 0) (a, d) = (a + d * (n - 1), -d);

		x -= a;
		var max = d * (n - 1);

		if (x <= 0) return -x;
		if (x >= max) return x - max;
		return Math.Min(x % d, d - x % d);
	}
}
