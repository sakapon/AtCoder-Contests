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

		[System.Diagnostics.DebuggerDisplay(@"\{Edge {Id} : {From.Id} --- {To.Id}\}")]
		public class Edge
		{
			public int Id { get; }
			public Vertex From { get; }
			public Vertex To { get; }

			public Edge(int id, Vertex from, Vertex to)
			{
				Id = id;
				From = from;
				To = to;
			}

			public Edge Reverse() => new Edge(Id, To, From);
		}

		public Vertex[] Vertexes { get; }
		public List<Edge> Edges { get; } = new List<Edge>();
		public int VertexesCount => Vertexes.Length;
		public int EdgesCount => Edges.Count;

		public UndirectedEdgeGraph(int vertexesCount, IEnumerable<(int u, int v)> edges = null)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
			if (edges != null) foreach (var (u, v) in edges) AddEdge(u, v);
		}

		public void AddEdge(int u, int v)
		{
			if (u > v) (u, v) = (v, u);
			var lv = Vertexes[u];
			var uv = Vertexes[v];
			var e = new Edge(Edges.Count, lv, uv);
			// 片側のみ登録します。
			Edges.Add(e);
			lv.Edges.Add(e);
			uv.Edges.Add(e.Reverse());
		}
	}
}
