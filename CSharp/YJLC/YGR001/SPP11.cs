using System;
using System.Collections.Generic;
using System.Linq;

class SPP11
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], s = h[2], t = h[3];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var (d, from) = Dijkstra(n, s, es, true);
		if (d[t] == long.MaxValue) { Console.WriteLine(-1); return; }

		var path = GetPathEdges(from, t);
		Console.WriteLine($"{d[t]} {path.Length}");
		Console.WriteLine(string.Join("\n", path.Select(e => $"{e.From} {e.To}")));
	}

	struct Edge
	{
		public int From, To, Weight;
	}

	static List<Edge>[] ToMap2(int n, int[][] es, bool directed = false)
	{
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<Edge>());
		foreach (var e in es)
		{
			map[e[0]].Add(new Edge { From = e[0], To = e[1], Weight = e[2] });
			if (!directed) map[e[1]].Add(new Edge { From = e[1], To = e[0], Weight = e[2] });
		}
		return map;
	}

	static (long[] d, int[] from) Dijkstra(int n, int sv, int[][] es, bool directed = false)
	{
		// 使われない頂点が存在してもかまいません (1-indexed でも可)。
		var map = ToMap2(n, es, directed);

		var d = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var from = Enumerable.Repeat(-1, n + 1).ToArray();
		var q = PQ<int>.CreateWithKey(v => d[v]);
		d[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var (v, qd) = q.Pop();
			if (d[v] < qd) continue;
			foreach (var e in map[v])
			{
				if (d[e.To] <= d[v] + e.Weight) continue;
				d[e.To] = d[v] + e.Weight;
				from[e.To] = v;
				q.Push(e.To);
			}
		}
		return (d, from);
	}

	static int[] GetPathVertexes(int[] from, int ev)
	{
		var path = new Stack<int>();
		for (var v = ev; v != -1; v = from[v])
			path.Push(v);
		return path.ToArray();
	}

	static Edge[] GetPathEdges(int[] from, int ev)
	{
		var path = new Stack<Edge>();
		for (var v = ev; from[v] != -1; v = from[v])
			path.Push(new Edge { From = from[v], To = v });
		return path.ToArray();
	}
}
