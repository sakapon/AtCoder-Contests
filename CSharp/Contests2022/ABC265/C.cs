using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var g = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var u = new bool[h, w];
		var (i, j) = (0, 0);
		u[i, j] = true;

		while (true)
		{
			var (ni, nj) = Next(i, j);
			if ((ni, nj) == (i, j)) return $"{i + 1} {j + 1}";

			(i, j) = (ni, nj);
			if (u[i, j]) return -1;
			u[i, j] = true;
		}

		(int, int) Next(int i, int j)
		{
			var c = g[i][j];
			if (c == 'U') return (i == 0 ? 0 : i - 1, j);
			else if (c == 'D') return (i == h - 1 ? h - 1 : i + 1, j);
			else if (c == 'L') return (i, j == 0 ? 0 : j - 1);
			else return (i, j == w - 1 ? w - 1 : j + 1);
		}
	}
}
