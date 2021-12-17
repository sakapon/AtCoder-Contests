using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, c) = Read3();

		for (int x = 0; x <= b; x += c)
		{
			if (a <= x)
			{
				return x;
			}
		}
		return -1;
	}
}
