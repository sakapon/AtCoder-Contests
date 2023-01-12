using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph211;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read3());
		var s = k == 0 ? new int[0] : Read();

		var graph = new WeightedGraph(2 * n + 1);
		foreach (var (u, v, a) in es)
		{
			graph.AddEdge(u + a * n, v + a * n, true, 1);
		}
		foreach (var v in s)
		{
			graph.AddEdge(v, v + n, true, 0);
		}
		graph.ShortestByModBFS(2, 1 + n);

		var r = Math.Min(graph[n].Cost, graph[n + n].Cost);
		if (r == long.MaxValue) return -1;
		return r;
	}
}
