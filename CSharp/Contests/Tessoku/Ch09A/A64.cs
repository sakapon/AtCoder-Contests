﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Arrays;

class A64
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = PathCore.ToWeightedMap(n + 1, es, true);
		var r = PathCore.Dijkstra(map, 1);
		return string.Join("\n", r[1..].Select(x => x == long.MaxValue ? -1 : x));
	}
}
