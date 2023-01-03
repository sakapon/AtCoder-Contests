using System;
using System.Collections.Generic;
using System.Linq;
using YJLib8.Graphs.Core.IntEdges;

class CycleDetection2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new ListEdgeGraph(n, es, false);
		var ev = graph.DetectCycle();

		if (ev == null)
		{
			Console.WriteLine(-1);
			return;
		}

		var pathes = ev.GetPathEdges();

		Console.WriteLine(pathes.Length);
		Console.WriteLine(string.Join("\n", pathes));
	}
}
