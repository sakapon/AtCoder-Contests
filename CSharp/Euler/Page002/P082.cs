using System;
using System.Collections.Generic;
using System.Linq;
using Page002.Lib.Dijkstra402;
using static Util;

class P082
{
	const string textUrl = "https://projecteuler.net/project/resources/p082_matrix.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = GetText(textUrl).Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.ToArray();
		var h = a.Length;
		var w = a[0].Length;
		var n = h * w;

		var graph = new Dijkstra(n + 2);

		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = i * w + j;
				graph.AddEdge(v - 1, v, false, a[i][j]);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = i * w + j;
				graph.AddEdge(v - w, v, false, a[i][j]);
				graph.AddEdge(v, v - w, false, a[i - 1][j]);
			}

		for (int i = 0; i < h; ++i)
		{
			graph.AddEdge(n, i * w, false, a[i][0]);
			graph.AddEdge(i * w + w - 1, n + 1, false, 0);
		}

		graph.Execute(n, n + 1);
		return graph[n + 1].Cost;
	}
}
