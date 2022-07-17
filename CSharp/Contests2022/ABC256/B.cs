using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var b = new int[8];

		foreach (var x in a)
		{
			b[0] = 1;

			for (int i = 3; i >= 0; i--)
			{
				b[i + x] += b[i];
				b[i] = 0;
			}
		}
		return b[4..].Sum();
	}
}
