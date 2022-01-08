using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var r = 500;

		for (int i = 1; i < s.Length; i++)
		{
			var c1 = s[i - 1];
			var c2 = s[i];

			if (c1 == c2)
			{
				r += 301;
			}
			else if ("12345".Contains(c1) == "12345".Contains(c2))
			{
				r += 210;
			}
			else
			{
				r += 100;
			}
		}

		return r;
	}
}
