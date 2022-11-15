using System;
using System.Collections.Generic;

// SortedSet を利用します。
namespace YGR001.Lib.Dijkstra402
{
	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class Dijkstra
	{
		public Vertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public Vertex this[int v] => Vertexes[v];

		public Dijkstra(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public Dijkstra(int vertexesCount, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public Dijkstra(int vertexesCount, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public void AddEdge(int from, int to, bool twoWay, long cost)
		{
			Vertexes[from].Edges.Add((to, cost));
			if (twoWay) Vertexes[to].Edges.Add((from, cost));
		}

		public void Execute(int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var q = new SortedSet<(long, int)> { (0, sv) };

			while (q.Count > 0)
			{
				var (c, v) = q.Min;
				q.Remove((c, v));
				if (v == ev) break;
				var vo = Vertexes[v];

				foreach (var (nv, cost) in vo.Edges)
				{
					var nvo = Vertexes[nv];
					var nc = c + cost;
					if (nvo.Cost <= nc) continue;
					if (nvo.Cost != long.MaxValue) q.Remove((nvo.Cost, nv));
					nvo.Cost = nc;
					nvo.Previous = vo;
					q.Add((nc, nv));
				}
			}
		}

		public Vertex[] GetPathVertexes(int ev)
		{
			var path = new Stack<Vertex>();
			for (var v = Vertexes[ev]; v != null; v = v.Previous)
				path.Push(v);
			return path.ToArray();
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<(int to, long cost)> Edges { get; } = new List<(int, long)>();
		public long Cost { get; set; } = long.MaxValue;
		public Vertex Previous { get; set; }
		public Vertex(int id) { Id = id; }
	}
}
