using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());

		var r = 0L;

		for (int i = 1; i < 5000000; i++)
		{
			r *= 10;
			r += 7;
			r %= k;

			if (r == 0) return i;
		}

		return -1;
	}
}
