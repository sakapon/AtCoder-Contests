using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var l = new List<(int, int)>();

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				l.Add((i, j));
			}
		}

		for (int s = 0; s < l.Count; s++)
		{
			var (i1, j1) = l[s];
			for (int t = s + 1; t < l.Count; t++)
			{
				var (i2, j2) = l[t];
				if (i1 >= i2 || j1 >= j2) continue;
				if (a[i1][j1] + a[i2][j2] > a[i2][j1] + a[i1][j2]) return false;
			}
		}
		return true;
	}
}
