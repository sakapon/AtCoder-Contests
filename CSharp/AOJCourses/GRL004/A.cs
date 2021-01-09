using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = DirectedGraph.TopologicalSort(h[0], es);
		Console.WriteLine(r == null ? 1 : 0);
	}
}

class DirectedGraph
{
	public static int[] TopologicalSort(int n, int[][] des)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		var indeg = new int[n];
		foreach (var e in des)
		{
			map[e[0]].Add(e);
			++indeg[e[1]];
		}

		var r = new List<int>();
		var q = new Queue<int>();
		var svs = Enumerable.Range(0, n).Where(v => indeg[v] == 0).ToArray();

		foreach (var sv in svs)
		{
			r.Add(sv);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					r.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}
		}
		if (r.Count < n) return null;
		return r.ToArray();
	}
}
