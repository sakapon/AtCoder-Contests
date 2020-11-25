using System;
using static System.Math;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b) = Read2();
		var (c, d) = Read2();

		if ((a, b) == (c, d))
			return 0;

		if (a + b == c + d || a - b == c - d || Abs(a - c) + Abs(b - d) <= 3)
			return 1;

		if (Abs(a - b) % 2 == Abs(c - d) % 2 || Abs(a - c) + Abs(b - d) <= 6 ||
			Abs(Abs(a - b) - Abs(c - d)) <= 3 || Abs(Abs(a + b) - Abs(c + d)) <= 3)
			return 2;

		return 3;
	}
}
