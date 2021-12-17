using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (a, b, c) = Read3();
		var k = int.Parse(Console.ReadLine());

		while (a >= b)
		{
			if (k-- == 0) return false;
			b *= 2;
		}
		while (b >= c)
		{
			if (k-- == 0) return false;
			c *= 2;
		}
		return true;
	}
}
