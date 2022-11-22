using System;
using System.Collections.Generic;

// 頂点を抽象化します。
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

	public class ListVertex<T> : Vertex<T>
	{
		public ListVertex(T id) : base(id) { }
		public List<ListVertex<T>> Edges { get; } = new List<ListVertex<T>>();
		public override IEnumerable<Vertex<T>> GetEdges() => Edges;
	}

	public class FuncVertex<T> : Vertex<T>
	{
		Func<FuncVertex<T>, IEnumerable<FuncVertex<T>>> getEdges;
		public FuncVertex(T id, Func<FuncVertex<T>, IEnumerable<FuncVertex<T>>> getEdges) : base(id) { this.getEdges = getEdges; }
		public override IEnumerable<Vertex<T>> GetEdges() => getEdges(this);
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class IntUnweightedGraph
	{
		public ListVertex<int>[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public ListVertex<int> this[int v] => Vertexes[v];

		public IntUnweightedGraph(int vertexesCount)
		{
			Vertexes = new ListVertex<int>[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new ListVertex<int>(v);
		}
		public IntUnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay) => AddEdge(Vertexes[from], Vertexes[to], twoWay);
		public void AddEdge(ListVertex<int> from, ListVertex<int> to, bool twoWay)
		{
			from.Edges.Add(to);
			if (twoWay) to.Edges.Add(from);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes) v.ClearResult();
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class UnweightedGraph<T>
	{
		public Dictionary<T, ListVertex<T>> Vertexes { get; } = new Dictionary<T, ListVertex<T>>();
		public int VertexesCount => Vertexes.Count;
		public ListVertex<T> this[T v] => Vertexes[v];

		public UnweightedGraph() { }
		public UnweightedGraph(IEnumerable<(T from, T to)> edges, bool twoWay)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public ListVertex<T> AddOrGetVertex(T v)
		{
			if (!Vertexes.TryGetValue(v, out var vo)) Vertexes[v] = vo = new ListVertex<T>(v);
			return vo;
		}

		public void AddEdge(T from, T to, bool twoWay) => AddEdge(AddOrGetVertex(from), AddOrGetVertex(to), twoWay);
		public void AddEdge(ListVertex<T> from, ListVertex<T> to, bool twoWay)
		{
			from.Edges.Add(to);
			if (twoWay) to.Edges.Add(from);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes.Values) v.ClearResult();
		}
	}
}
