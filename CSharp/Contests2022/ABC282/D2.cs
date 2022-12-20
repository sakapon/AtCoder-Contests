using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Specialized.Int;
using CoderLib8.Graphs.Specialized.Int.BipartiteGraph201;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new ListUnweightedGraph(n + 1, es, true);
		var vs = graph.BipartiteForest();
		if (vs == null) return 0;

		var r = (long)n * n - es.Length * 2;

		foreach (var g in vs[1..].GroupBy(v => v.Root.Id))
		{
			var c0 = 0L;
			var c1 = 0L;

			foreach (var v in g)
			{
				if (v.Color == 0) c0++;
				else c1++;
			}

			r -= c0 * c0;
			r -= c1 * c1;
		}
		return r / 2;
	}
}
