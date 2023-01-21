using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var z = Read();
		var a = Read();

		var b = (int[])a.Clone();

		var length = z[2] - z[1] + 1;
		for (int i = 0; i < length; i++)
		{
			b[z[1] - 1 + i] = a[z[3] - 1 + i];
			b[z[3] - 1 + i] = a[z[1] - 1 + i];
		}
		return string.Join(" ", b);
	}
}
