﻿using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var x = int.Parse(Console.ReadLine());
		if (x == 0) return false;
		return x % 100 == 0;
	}
}
