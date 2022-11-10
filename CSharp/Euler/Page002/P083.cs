using System;
using System.Collections.Generic;
using System.Linq;
using Page002.Lib.Dijkstra402;
using static Util;

class P083
{
	const string textUrl = "https://projecteuler.net/project/resources/p083_matrix.txt";
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = GetText(textUrl).Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
			.ToArray();
		var h = a.Length;
		var w = a[0].Length;
		var n = h * w;

		var graph = new Dijkstra(n);

		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = i * w + j;
				graph.AddEdge(v - 1, v, false, a[i][j]);
				graph.AddEdge(v, v - 1, false, a[i][j - 1]);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = i * w + j;
				graph.AddEdge(v - w, v, false, a[i][j]);
				graph.AddEdge(v, v - w, false, a[i - 1][j]);
			}

		graph.Execute(0, n - 1);
		return a[0][0] + graph[n - 1].Cost;
	}
}
