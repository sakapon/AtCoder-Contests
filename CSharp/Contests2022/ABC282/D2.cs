using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Int.BipartiteGraph101;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var r = 0L;
		var graph = new BipartiteGraph(n + 1, es, true);
		var vs = graph.BFSForest();
		if (vs == null) return 0;

		foreach (var g in vs[1..].GroupBy(v => v.Root.Id))
		{
			var gvs = g.ToArray();

			var counts = new long[2];
			counts[0] = gvs.Count(v => !v.Color);
			counts[1] = gvs.Length - counts[0];

			foreach (var v in gvs)
			{
				r += n - counts[v.Color ? 1 : 0] - v.Edges.Count;
			}
		}
		return r / 2;
	}
}
