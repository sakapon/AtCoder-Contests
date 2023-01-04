using System;
using System.Collections.Generic;

// GetEdges を抽象化します。
// 実行結果は入力グラフから分離されます。
namespace CoderLib8.Graphs.SPPs.Typed.UnweightedGraph402
{
	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public abstract class UnweightedGraph<T>
	{
		public abstract int VertexesCount { get; }
		public abstract IEnumerable<T> GetVertexes();
		public abstract List<T> GetEdges(T v);
	}

	public class ListUnweightedGraph<T> : UnweightedGraph<T>
	{
		protected readonly Dictionary<T, List<T>> map;
		public Dictionary<T, List<T>> AdjacencyList => map;
		public override int VertexesCount => map.Count;
		public override IEnumerable<T> GetVertexes() => map.Keys;
		public override List<T> GetEdges(T v) => map[v];

		public ListUnweightedGraph(Dictionary<T, List<T>> map = null) { this.map = map ?? new Dictionary<T, List<T>>(); }
		public ListUnweightedGraph(IEnumerable<(T from, T to)> edges, bool twoWay) : this()
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public bool AddVertex(T v)
		{
			if (map.ContainsKey(v)) return false;
			map[v] = new List<T>();
			return true;
		}

		public void AddEdge(T from, T to, bool twoWay)
		{
			if (!map.TryGetValue(from, out var edges)) map[from] = edges = new List<T>();
			edges.Add(to);
			if (twoWay)
			{
				if (!map.TryGetValue(to, out edges)) map[to] = edges = new List<T>();
				edges.Add(from);
			}
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: Cost = {Cost}\}")]
	public class Vertex<T>
	{
		public T Id { get; }
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex<T> Parent { get; set; }
		public Vertex(T id) { Id = id; }

		public T[] GetPathVertexes()
		{
			var path = new Stack<T>();
			for (var v = this; v != null; v = v.Parent)
				path.Push(v.Id);
			return path.ToArray();
		}

		public (T, T)[] GetPathEdges()
		{
			var path = new Stack<(T, T)>();
			for (var v = this; v.Parent != null; v = v.Parent)
				path.Push((v.Parent.Id, v.Id));
			return path.ToArray();
		}
	}

	public static class UnweightedGraphEx
	{
		static Dictionary<T, Vertex<T>> CreateVertexes<T>(this UnweightedGraph<T> graph)
		{
			var vs = new Dictionary<T, Vertex<T>>();
			foreach (var v in graph.GetVertexes()) vs[v] = new Vertex<T>(v);
			return vs;
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS、BFS または Union-Find を利用します。
		public static Dictionary<T, Vertex<T>> ConnectivityByDFS<T>(this UnweightedGraph<T> graph, T sv, T ev)
		{
			var vs = graph.CreateVertexes();
			vs[sv].Cost = 0;
			DFS(sv);
			return vs;

			bool DFS(T v)
			{
				if (vs.Comparer.Equals(v, ev)) return true;
				var vo = vs[v];

				foreach (var nv in graph.GetEdges(v))
				{
					var nvo = vs[nv];
					if (nvo.Cost != long.MaxValue) continue;
					nvo.Cost = vo.Cost + 1;
					nvo.Parent = vo;
					if (DFS(nv)) return true;
				}
				return false;
			}
		}

		public static Dictionary<T, Vertex<T>> ShortestByBFS<T>(this UnweightedGraph<T> graph, T sv, T ev)
		{
			var vs = graph.CreateVertexes();
			vs[sv].Cost = 0;
			var q = new Queue<T>();
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (vs.Comparer.Equals(v, ev)) return vs;
				var vo = vs[v];
				var nc = vo.Cost + 1;

				foreach (var nv in graph.GetEdges(v))
				{
					var nvo = vs[nv];
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Parent = vo;
					q.Enqueue(nv);
				}
			}
			return vs;
		}
	}
}
