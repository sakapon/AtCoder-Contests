using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph211;
using static EulerLib8.Common;

class P083
{
	const string textUrl = "https://projecteuler.net/project/resources/p083_matrix.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = GetText(textUrl).Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.ToArray();
		var h = s.Length;
		var w = s[0].Length;
		var n = h * w;

		var grid = new WeightedGridHelper(h, w);
		var graph = grid.GetWeightedAdjacencyList(s);
		graph.Dijkstra(0, n - 1);
		return s[0][0] + graph[n - 1].Cost;
	}
}
