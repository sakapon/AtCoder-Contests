using System;
using System.Collections.Generic;
using System.Linq;
using YJLib8.Graphs.Core.IntEdges;

class CycleDetectionU
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new ListEdgeGraph(n, es, true);
		var ev = graph.DetectCycle();

		if (ev == null)
		{
			Console.WriteLine(-1);
			return;
		}

		var pathvs = ev.GetPathVertexes();
		var pathes = ev.GetPathEdges();

		Console.WriteLine(pathvs.Length);
		Console.WriteLine(string.Join(" ", pathvs[0..^1].Prepend(pathvs[^1])));
		Console.WriteLine(string.Join(" ", pathes));
	}
}
