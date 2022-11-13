using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Int.Edges
{
	/// <summary>
	/// グラフを表します。辺をオブジェクトとして扱います。
	/// </summary>
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

		// 異なる id の辺として登録します。
		public void AddTwoWayDifferentEdges(int from, int to, long cost = 1)
		{
			var fv = Vertexes[from];
			var tv = Vertexes[to];

			var e1 = new Edge(Edges.Count, fv, tv, false, cost);
			Edges.Add(e1);
			fv.Edges.Add(e1);

			var e2 = new Edge(Edges.Count, tv, fv, false, cost);
			Edges.Add(e2);
			tv.Edges.Add(e2);
		}
	}
}
