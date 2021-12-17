using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Grid.Spp;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = GraphConsole.ReadIntGrid(h);
		var b = GraphConsole.ReadIntGrid(h - 1);

		Point sv = (0, 0);
		Point ev = (h - 1, w - 1);

		var r = ShortestPathCore.Dijkstra(h * 2, w, v =>
		{
			var es = new List<Edge>();

			if (v.i < h)
			{
				if (v.j < w - 1) es.Add(new Edge(v, v + (0, 1), a[v]));
				if (v.j > 0) es.Add(new Edge(v, v - (0, 1), a[v - (0, 1)]));
				if (v.i < h - 1) es.Add(new Edge(v, v + (1, 0), b[v]));
				es.Add(new Edge(v, v + (h, 0), 1));
			}
			else
			{
				if (v.i > h) es.Add(new Edge(v, v - (1, 0), 1));
				es.Add(new Edge(v, v - (h, 0), 0));
			}

			return es.ToArray();
		}, sv, ev);

		return r[ev];
	}
}
