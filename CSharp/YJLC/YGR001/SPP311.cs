using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.EdgeGraph311;

class SPP311
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, s, t) = Read4();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var graph = new EdgeGraph(n, es, false);
		graph.Dijkstra(s, t);
		if (!graph[t].IsConnected) return -1;

		var path = graph.GetPathEdges(t);
		return $"{graph[t].Cost} {path.Length}\n" + string.Join("\n", path.Select(e => $"{e.From.Id} {e.To.Id}"));
	}
}
