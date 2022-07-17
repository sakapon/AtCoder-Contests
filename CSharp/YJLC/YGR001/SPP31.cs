using System;
using System.Collections.Generic;
using System.Linq;

class SPP31
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, s, t) = Read4();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var spp = new SppWeightedGraph(n);
		spp.AddEdges(es, true);
		var (d, from) = spp.Dijkstra(s, t);
		if (d[t] == long.MaxValue) return -1;

		var path = GetPathVertexes(from, t);
		return $"{d[t]} {path.Length - 1}\n" + string.Join("\n", Enumerable.Range(0, path.Length - 1).Select(i => $"{path[i]} {path[i + 1]}"));
	}

	static int[] GetPathVertexes(int[] from, int ev)
	{
		var path = new Stack<int>();
		for (var v = ev; v != -1; v = from[v])
			path.Push(v);
		return path.ToArray();
	}
}
