﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => { Console.ReadLine(); return Read(); });

		var graph = new ListUnweightedGraph(m + n + 1);

		for (int i = 1; i <= n; i++)
		{
			foreach (var v in s[i - 1])
			{
				graph.AddEdge(v, m + i, true);
			}
		}

		var r = graph.ShortestByBFS(1, m);
		if (r[m] == long.MaxValue) return -1;
		return r[m] / 2 - 1;
	}
}
