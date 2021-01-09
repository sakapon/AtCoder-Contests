using System;
using System.Collections.Generic;
using System.Linq;

class A21
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var ts = DirectedGraph.TopologicalSort(h[0], es);
		var r = ts == null ? BellmanFord(h[0], es, h[2]) : DPforTS(h[0], es, h[2], ts);
		Console.WriteLine(string.Join("\n", r.Item1.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static Tuple<long[], int[][]> BellmanFord(int n, int[][] des, int sv)
	{
		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		cs[sv] = 0;

		var next = true;
		for (int k = 0; k < n && next; ++k)
		{
			next = false;
			foreach (var e in des)
			{
				if (cs[e[0]] == long.MaxValue || cs[e[1]] <= cs[e[0]] + e[2]) continue;
				cs[e[1]] = cs[e[0]] + e[2];
				inEdges[e[1]] = e;
				next = true;
			}
		}
		if (next) return Tuple.Create<long[], int[][]>(null, null);
		return Tuple.Create(cs, inEdges);
	}

	static Tuple<long[], int[][]> DPforTS(int n, int[][] des, int sv, int[] ts)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in des)
		{
			map[e[0]].Add(e);
		}

		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		cs[sv] = 0;

		foreach (var v in ts)
		{
			if (cs[v] == long.MaxValue) continue;
			foreach (var e in map[v])
			{
				if (cs[e[1]] <= cs[v] + e[2]) continue;
				cs[e[1]] = cs[v] + e[2];
				inEdges[e[1]] = e;
			}
		}
		return Tuple.Create(cs, inEdges);
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
