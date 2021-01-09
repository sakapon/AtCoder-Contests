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
		a = Abs(a - c);
		b = Abs(b - d);

		if ((a, b) == (0, 0))
			return 0;
		if (a == b || a + b <= 3)
			return 1;
		if (a % 2 == b % 2 || a + b <= 6 || Abs(a - b) <= 3)
			return 2;
		return 3;
	}
}
