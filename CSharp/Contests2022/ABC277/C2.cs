using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Typed.UnweightedGraph221;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read2());

		var graph = new UnweightedGraph<int>(es, true);
		graph.ConnectivityByDFS(1, -1);
		return graph.Vertexes.Values.Where(v => v.IsConnected).Max(v => v.Id);
	}
}
