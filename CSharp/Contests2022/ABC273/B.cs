using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, k) = Read2L();
		var d = (decimal)x;

		for (int i = 0; i < k; i++)
		{
			d /= 10;
			d = Math.Round(d, MidpointRounding.AwayFromZero);
		}
		for (int i = 0; i < k; i++)
		{
			d *= 10;
		}
		return d;
	}
}
