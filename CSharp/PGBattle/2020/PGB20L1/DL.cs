using System;
using System.Collections.Generic;
using System.Linq;

class DL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var graph = new EdgeGraph(n + 1, es, false);
		var (c, p) = graph.BFS(1, n);
		if (c[n] == long.MaxValue) return -1;
		return string.Join(" ", GetPathVertexes(p, n));
	}

	public static int[] GetPathVertexes(int[] p, int ev)
	{
		var path = new Stack<int>();
		for (; ev != -1; ev = p[ev])
			path.Push(ev);
		return path.ToArray();
	}
}

[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
public class EdgeGraph
{
	[System.Diagnostics.DebuggerDisplay(@"\{Vertex {Id} : {Edges.Count} edges\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<Edge> Edges { get; } = new List<Edge>();

		public Vertex(int id)
		{
			Id = id;
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"\{Edge {Id} : {From.Id} --> {To.Id}, Cost = {Cost}\}")]
	public class Edge
	{
		public int Id { get; }
		public Vertex From { get; }
		public Vertex To { get; }
		public long Cost { get; }
		public Edge Reverse { get; private set; }

		public Edge(int id, Vertex from, Vertex to, bool twoWay, long cost = 1)
		{
			Id = id;
			From = from;
			To = to;
			Cost = cost;
			if (twoWay) Reverse = new Edge(id, to, from, false, cost) { Reverse = this };
		}
	}

	readonly int n;
	// id -> vertex
	public Vertex[] Vertexes { get; }
	// id -> edge
	public List<Edge> Edges { get; } = new List<Edge>();
	public int VertexesCount => n;
	public int EdgesCount => Edges.Count;

	public Vertex this[int v] => Vertexes[v];

	public EdgeGraph(int vertexesCount)
	{
		n = vertexesCount;
		Vertexes = new Vertex[vertexesCount];
		for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
	}
	public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
	{
		foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
	}
	public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(vertexesCount)
	{
		foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
	}
	public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(vertexesCount)
	{
		foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
	}

	public void AddEdge(int from, int to, bool twoWay, long cost = 1)
	{
		var fv = Vertexes[from];
		var tv = Vertexes[to];

		var e1 = new Edge(Edges.Count, fv, tv, twoWay, cost);
		Edges.Add(e1);
		fv.Edges.Add(e1);
		if (twoWay) tv.Edges.Add(e1.Reverse);
	}

	public (long[] costs, int[] prevs) BFS(int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var prevs = Array.ConvertAll(costs, _ => -1);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			// 改造
			foreach (var ne in Vertexes[v].Edges.OrderBy(e => e.To.Id))
			{
				var nv = ne.To.Id;
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				prevs[nv] = v;
				if (nv == ev) return (costs, prevs);
				q.Enqueue(nv);
			}
		}
		return (costs, prevs);
	}
}
