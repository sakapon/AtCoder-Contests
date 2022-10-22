using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int.Edges
{
	/// <summary>
	/// 有向グラフを表します。
	/// </summary>
	[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
	public class DirectedEdgeGraph
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

			public Edge(int id, Vertex from, Vertex to, long cost = 1)
			{
				Id = id;
				From = from;
				To = to;
				Cost = cost;
			}
		}

		public Vertex[] Vertexes { get; }
		public List<Edge> Edges { get; } = new List<Edge>();
		public int VertexesCount => Vertexes.Length;
		public int EdgesCount => Edges.Count;

		public DirectedEdgeGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public DirectedEdgeGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay = false) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay: twoWay);
		}
		public DirectedEdgeGraph(int vertexesCount, IEnumerable<(int from, int to, long cost)> edges, bool twoWay = false) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, cost, twoWay);
		}

		public void AddEdge(int from, int to, long cost = 1, bool twoWay = false)
		{
			var fv = Vertexes[from];
			var tv = Vertexes[to];

			var e1 = new Edge(Edges.Count, fv, tv, cost);
			Edges.Add(e1);
			fv.Edges.Add(e1);

			// 異なる辺として登録します。
			if (twoWay)
			{
				var e2 = new Edge(Edges.Count, tv, fv, cost);
				Edges.Add(e2);
				tv.Edges.Add(e2);
			}
		}
	}
}
