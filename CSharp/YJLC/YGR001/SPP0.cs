﻿using System;
using System.Collections.Generic;
using System.Linq;

class SPP0
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], s = h[2], t = h[3];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var (d, inEdges) = ShortestPath0.Dijkstra(n, es, true, s, t);
		if (inEdges[t] == null) { Console.WriteLine(-1); return; }

		var path = ShortestPath0.GetPathEdges(inEdges, t);
		Console.WriteLine($"{d[t]} {path.Length}");
		Console.WriteLine(string.Join("\n", path.Select(e => $"{e[0]} {e[1]}")));
	}
}
