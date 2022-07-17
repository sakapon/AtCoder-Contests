using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "Yes" : "No")));
	static bool Solve()
	{
		var (a, s) = Read2L();

		long x = 0, y = 0, f = 0;

		for (int i = 0; i < 60; i++)
		{
			if ((a & (1L << i)) == 0)
			{
				f += 1L << i;
			}
			else
			{
				x += 1L << i;
				y += 1L << i;
			}
		}
		return ((s - x - y) | f) == f;
	}
}
