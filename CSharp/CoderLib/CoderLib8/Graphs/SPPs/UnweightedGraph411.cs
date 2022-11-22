using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.UnweightedGraph411
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: Cost = {Cost}\}")]
	public abstract class Vertex<T>
	{
		public T Id { get; }
		public abstract IEnumerable<Vertex<T>> GetEdges();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex<T> Previous { get; set; }
		protected Vertex(T id) { Id = id; }

		public void ClearResult()
		{
			Cost = long.MaxValue;
			Previous = null;
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS または Union-Find を利用します。
		public Vertex<T> ConnectivityByDFS(Vertex<T> ev)
		{
			Cost = 0;
			var q = new Stack<Vertex<T>>();
			q.Push(this);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (v == ev) return ev;

				foreach (var nv in v.GetEdges())
				{
					if (nv.Cost == 0) continue;
					nv.Cost = 0;
					nv.Previous = v;
					q.Push(nv);
				}
			}
			return ev;
		}

		public Vertex<T> ShortestByBFS(Vertex<T> ev)
		{
			Cost = 0;
			var q = new Queue<Vertex<T>>();
			q.Enqueue(this);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (v == ev) return ev;
				var nc = v.Cost + 1;

				foreach (var nv in v.GetEdges())
				{
					if (nv.Cost <= nc) continue;
					nv.Cost = nc;
					nv.Previous = v;
					q.Enqueue(nv);
				}
			}
			return ev;
		}

		public Vertex<T>[] GetPathVertexes()
		{
			var path = new Stack<Vertex<T>>();
			for (var v = this; v != null; v = v.Previous)
				path.Push(v);
			return path.ToArray();
		}

		public (Vertex<T>, Vertex<T>)[] GetPathEdges()
		{
			var path = new Stack<(Vertex<T>, Vertex<T>)>();
			for (var v = this; v.Previous != null; v = v.Previous)
				path.Push((v.Previous, v));
			return path.ToArray();
		}
	}

	public class IntVertex : Vertex<int>
	{
		public IntVertex(int id) : base(id) { }
		public List<IntVertex> Edges { get; } = new List<IntVertex>();
		public override IEnumerable<Vertex<int>> GetEdges() => Edges;
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class IntUnweightedGraph
	{
		public IntVertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public IntVertex this[int v] => Vertexes[v];

		public IntUnweightedGraph(int vertexesCount)
		{
			Vertexes = new IntVertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new IntVertex(v);
		}
		public IntUnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay) => AddEdge(Vertexes[from], Vertexes[to], twoWay);
		public void AddEdge(IntVertex from, IntVertex to, bool twoWay)
		{
			from.Edges.Add(to);
			if (twoWay) to.Edges.Add(from);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes) v.ClearResult();
		}
	}
}
