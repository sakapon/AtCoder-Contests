using System;
using System.Collections.Generic;

namespace YJLib8.Graphs.Core.IntEdges
{
	[System.Diagnostics.DebuggerDisplay(@"\{Edge {Id} : {From} --> {To}, Cost = {Cost}\}")]
	public class Edge
	{
		public int Id { get; }
		public int From { get; }
		public int To { get; }
		public long Cost { get; }
		public Edge Reverse { get; private set; }

		public Edge(int id, int from, int to, bool twoWay, long cost = 1)
		{
			Id = id;
			From = from;
			To = to;
			Cost = cost;
			if (twoWay) Reverse = new Edge(id, to, from, false, cost) { Reverse = this };
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public abstract class EdgeGraph
	{
		protected readonly int n;
		public int VertexesCount => n;
		public abstract List<Edge> GetEdges(int v);
		protected EdgeGraph(int n) { this.n = n; }
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
	public class ListEdgeGraph : EdgeGraph
	{
		protected readonly List<Edge>[] map;
		public List<Edge>[] AdjacencyList => map;
		public override List<Edge> GetEdges(int v) => map[v];

		// id -> edge
		public List<Edge> Edges { get; } = new List<Edge>();
		public int EdgesCount => Edges.Count;

		public ListEdgeGraph(int n) : base(n)
		{
			map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
		}
		public ListEdgeGraph(int n, IEnumerable<(int from, int to)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}
		public ListEdgeGraph(int n, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public ListEdgeGraph(int n, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public void AddEdge(int from, int to, bool twoWay, long cost = 1)
		{
			var e1 = new Edge(Edges.Count, from, to, twoWay, cost);
			Edges.Add(e1);
			map[from].Add(e1);
			if (twoWay) map[to].Add(e1.Reverse);
		}

		// 異なる id の辺として登録します。
		public void AddTwoWayDifferentEdges(int from, int to, long cost = 1)
		{
			var e1 = new Edge(Edges.Count, from, to, false, cost);
			Edges.Add(e1);
			map[from].Add(e1);

			var e2 = new Edge(Edges.Count, to, from, false, cost);
			Edges.Add(e2);
			map[to].Add(e2);
		}
	}
}
