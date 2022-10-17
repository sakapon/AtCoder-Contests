using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var (i, j, qi, qj) = Read4();
		i--; j--; qi--; qj--;
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		// U, R, D, L
		var d = 0;
		var u = new bool[h, w, 4];
		u[i, j, d] = true;

		var vs = new[] { (-1, 0), (0, 1), (1, 0), (0, -1) };

		for (int k = 1; ; k++)
		{
			for (int e = -1; e <= 2; e++)
			{
				var nd = (d + e + 4) % 4;
				var (di, dj) = vs[nd];
				var ni = i + di;
				var nj = j + dj;

				if (ni < 0 || h <= ni) continue;
				if (nj < 0 || w <= nj) continue;

				if (s[ni][nj] == '.')
				{
					(i, j, d) = (ni, nj, nd);
					break;
				}
			}

			if ((i, j) == (qi, qj)) return k;
			if (u[i, j, d]) return -1;
			u[i, j, d] = true;
		}
	}
}
