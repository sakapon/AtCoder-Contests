﻿using System;
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
		var x = Console.ReadLine();
		var n = x.Length;

		if (!x.Contains('0')) return x;

		var i = x.IndexOf('0');
		return x[..i] + new string('1', n - i);
	}
}
