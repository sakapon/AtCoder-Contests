using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w, n) = Read3();
		var a = Array.ConvertAll(new bool[h], _ => Read());
		var c = Read();

		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var s1 = a[i][j];
				var s2 = a[i][j - 1];
				if (s1 != s2 && c[s1 - 1] == c[s2 - 1]) return false;
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var s1 = a[i][j];
				var s2 = a[i - 1][j];
				if (s1 != s2 && c[s1 - 1] == c[s2 - 1]) return false;
			}
		return true;
	}
}
