using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c) = Read3();

		double f(double x) => a * x * x * x * x * x + b * x + c;
		return First(1, 2, x => f(x) >= 0);
	}

	static double First(double l, double r, Func<double, bool> f, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
		return r;
	}
}
