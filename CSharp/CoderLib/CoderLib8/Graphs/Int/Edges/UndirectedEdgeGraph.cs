using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int.Edges
{
	/// <summary>
	/// 無向グラフを表します。
	/// </summary>
	[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
	public class UndirectedEdgeGraph
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
			public Edge Reverse { get; internal set; }

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

		public UndirectedEdgeGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public UndirectedEdgeGraph(int vertexesCount, IEnumerable<(int u, int v)> edges) : this(vertexesCount)
		{
			foreach (var (u, v) in edges) AddEdge(u, v);
		}
		public UndirectedEdgeGraph(int vertexesCount, IEnumerable<(int u, int v, long cost)> edges) : this(vertexesCount)
		{
			foreach (var (u, v, cost) in edges) AddEdge(u, v, cost);
		}

		public void AddEdge(int u, int v, long cost = 1)
		{
			var fv = Vertexes[u];
			var tv = Vertexes[v];

			var e1 = new Edge(Edges.Count, fv, tv, cost);
			var e2 = new Edge(Edges.Count, tv, fv, cost);
			e1.Reverse = e2;
			e2.Reverse = e1;

			// 片側のみ登録します。
			Edges.Add(e1);
			fv.Edges.Add(e1);
			tv.Edges.Add(e2);
		}
	}
}
