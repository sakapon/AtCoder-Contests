using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph211;
using static EulerLib8.Common;

class P082
{
	const string textUrl = "https://projecteuler.net/project/resources/p082_matrix.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = GetText(textUrl).Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.ToArray();
		var h = s.Length;
		var w = s[0].Length;
		var n = h * w;

		var graph = new WeightedGraph(n + 2);

		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = w * i + j;
				graph.AddEdge(v - 1, v, false, s[i][j]);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = w * i + j;
				graph.AddEdge(v, v - w, false, s[i - 1][j]);
				graph.AddEdge(v - w, v, false, s[i][j]);
			}

		for (int i = 0; i < h; ++i)
		{
			graph.AddEdge(n, i * w, false, s[i][0]);
			graph.AddEdge(i * w + w - 1, n + 1, false, 0);
		}

		graph.Dijkstra(n, n + 1);
		return graph[n + 1].Cost;
	}
}
