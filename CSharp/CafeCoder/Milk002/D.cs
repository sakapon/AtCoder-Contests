using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		var t = long.Parse(Console.ReadLine());

		t %= 2 * (n - 1);

		if (t < n - 1)
		{
			return 1 + t;
		}
		else
		{
			t -= n - 1;
			return n - t;
		}
	}
}
