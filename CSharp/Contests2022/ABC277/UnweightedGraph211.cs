using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Typed.UnweightedGraph211
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex<T>
	{
		public T Id { get; }
		public List<Vertex<T>> Edges { get; } = new List<Vertex<T>>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex<T> Previous { get; set; }
		public Vertex(T id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class UnweightedGraph<T>
	{
		public Dictionary<T, Vertex<T>> Vertexes { get; } = new Dictionary<T, Vertex<T>>();
		public int VertexesCount => Vertexes.Count;
		public Vertex<T> this[T v] => Vertexes[v];

		public UnweightedGraph() { }
		public UnweightedGraph(IEnumerable<(T from, T to)> edges, bool twoWay)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public Vertex<T> AddOrGetVertex(T v)
		{
			if (!Vertexes.TryGetValue(v, out var vo)) Vertexes[v] = vo = new Vertex<T>(v);
			return vo;
		}

		public void AddEdge(T from, T to, bool twoWay)
		{
			var fv = AddOrGetVertex(from);
			var tv = AddOrGetVertex(to);
			fv.Edges.Add(tv);
			if (twoWay) tv.Edges.Add(fv);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes.Values)
			{
				v.Cost = long.MaxValue;
				v.Previous = null;
			}
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS または Union-Find を利用します。
		public void ConnectivityByDFS(T sv, T ev)
		{
			if (!Vertexes.TryGetValue(sv, out var svo)) return;
			Vertexes.TryGetValue(ev, out var evo);

			svo.Cost = 0;
			var q = new Stack<Vertex<T>>();
			q.Push(svo);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var nv in v.Edges)
				{
					if (nv.Cost == 0) continue;
					nv.Cost = 0;
					nv.Previous = v;
					if (nv == evo) return;
					q.Push(nv);
				}
			}
		}

		public void ShortestByBFS(T sv, T ev)
		{
			if (!Vertexes.TryGetValue(sv, out var svo)) return;
			Vertexes.TryGetValue(ev, out var evo);

			svo.Cost = 0;
			var q = new Queue<Vertex<T>>();
			q.Enqueue(svo);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = v.Cost + 1;

				foreach (var nv in v.Edges)
				{
					if (nv.Cost <= nc) continue;
					nv.Cost = nc;
					nv.Previous = v;
					if (nv == evo) return;
					q.Enqueue(nv);
				}
			}
		}

		public Vertex<T>[] GetPathVertexes(T ev)
		{
			var path = new Stack<Vertex<T>>();
			for (var v = Vertexes[ev]; v != null; v = v.Previous)
				path.Push(v);
			return path.ToArray();
		}

		public (Vertex<T>, Vertex<T>)[] GetPathEdges(T ev)
		{
			var path = new Stack<(Vertex<T>, Vertex<T>)>();
			for (var v = Vertexes[ev]; v.Previous != null; v = v.Previous)
				path.Push((v.Previous, v));
			return path.ToArray();
		}
	}
}
