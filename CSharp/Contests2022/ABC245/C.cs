﻿using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, k) = Read2();
		var a = Read();
		var b = Read();

		var (da, db) = (true, true);
		var (ta, tb) = (true, true);

		for (int i = 1; i < n; i++)
		{
			ta = da && Math.Abs(a[i] - a[i - 1]) <= k || db && Math.Abs(a[i] - b[i - 1]) <= k;
			tb = da && Math.Abs(b[i] - a[i - 1]) <= k || db && Math.Abs(b[i] - b[i - 1]) <= k;

			(da, db) = (ta, tb);
		}

		return da || db;
	}
}
