using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.EdgeGraph311;

class A62E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new EdgeGraph(n + 1, es, true);
		graph.ConnectivityByDFS(1);
		return $"The graph is {(graph.Vertexes[1..].All(v => v.IsConnected) ? "" : "not ")}connected.";
	}
}
