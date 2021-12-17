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
		var (s, t) = Read2();

		var r = 0;

		for (int a = 0; a <= s; a++)
		{
			for (int b = 0; b <= s; b++)
			{
				for (int c = 0; c <= s; c++)
				{
					if (a + b + c <= s && a * b * c <= t)
					{
						r++;
					}
				}
			}
		}

		return r;
	}
}
