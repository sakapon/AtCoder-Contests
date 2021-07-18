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
		var n = long.Parse(Console.ReadLine());

		var p3 = PowsL(3, 30);

		for (int k = 1; k <= 30; k++)
		{
			var x = p3[k] + 1;

			for (int i = 0; i < 30 - k; i++)
			{
				x *= 3;
			}

			if (x == n)
			{
				return k;
			}
		}
		return -1;
	}

	public static long[] PowsL(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
