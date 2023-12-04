using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, s, m, l) = Read4();

		var r = 1 << 30;
		for (int i = 0; i < 20; i++)
			for (int j = 0; j < 20; j++)
				for (int k = 0; k < 20; k++)
					if (6 * i + 8 * j + 12 * k >= n)
						r = Math.Min(r, s * i + m * j + l * k);
		return r;
	}
}
