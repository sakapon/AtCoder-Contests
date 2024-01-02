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
		var d = Read();

		var r = 0;
		for (int i = 1; i <= 9; i++)
		{
			var i2 = int.Parse($"{i}{i}");

			if (i <= n)
			{
				if (i <= d[i - 1]) r++;
				if (i2 <= d[i - 1]) r++;
			}
			if (i2 <= n)
			{
				if (i <= d[i2 - 1]) r++;
				if (i2 <= d[i2 - 1]) r++;
			}
		}
		return r;
	}
}
