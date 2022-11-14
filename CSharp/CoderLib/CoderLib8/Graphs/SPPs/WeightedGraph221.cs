using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Typed.WeightedGraph221
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex<T>
	{
		public T Id { get; }
		public List<(Vertex<T> to, long cost)> Edges { get; } = new List<(Vertex<T>, long)>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex<T> Previous { get; set; }
		public Vertex(T id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class WeightedGraph<T>
	{
		public Dictionary<T, Vertex<T>> Vertexes { get; } = new Dictionary<T, Vertex<T>>();
		public int VertexesCount => Vertexes.Count;
		public Vertex<T> this[T v] => Vertexes[v];

		public WeightedGraph() { }
		public WeightedGraph(IEnumerable<(T from, T to, int cost)> edges, bool twoWay)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public WeightedGraph(IEnumerable<(T from, T to, long cost)> edges, bool twoWay)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public Vertex<T> AddOrGetVertex(T v)
		{
			if (!Vertexes.TryGetValue(v, out var vo)) Vertexes[v] = vo = new Vertex<T>(v);
			return vo;
		}

		public void AddEdge(T from, T to, bool twoWay, long cost)
		{
			var fv = AddOrGetVertex(from);
			var tv = AddOrGetVertex(to);
			fv.Edges.Add((tv, cost));
			if (twoWay) tv.Edges.Add((fv, cost));
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes.Values)
			{
				v.Cost = long.MaxValue;
				v.Previous = null;
			}
		}

		public void Dijkstra(T sv, T ev)
		{
			// 始点が存在しない場合には追加します。
			var svo = AddOrGetVertex(sv);
			Vertexes.TryGetValue(ev, out var evo);

			svo.Cost = 0;
			var comp = Comparer<(long c, Vertex<T> v)>.Create((x, y) =>
			{
				var d = x.c.CompareTo(y.c);
				if (d != 0) return d;
				return x.v.GetHashCode().CompareTo(y.v.GetHashCode());
			});
			var q = new SortedSet<(long, Vertex<T>)>(comp) { (0, svo) };

			while (q.Count > 0)
			{
				var (c, v) = q.Min;
				q.Remove((c, v));
				if (v == evo) return;

				foreach (var (nv, cost) in v.Edges)
				{
					var nc = c + cost;
					if (nv.Cost <= nc) continue;
					if (nv.Cost != long.MaxValue) q.Remove((nv.Cost, nv));
					q.Add((nc, nv));
					nv.Cost = nc;
					nv.Previous = v;
				}
			}
		}

		// Dijkstra 法の特別な場合です。
		public void ShortestByModBFS(int mod, T sv, T ev)
		{
			// 始点が存在しない場合には追加します。
			var svo = AddOrGetVertex(sv);
			Vertexes.TryGetValue(ev, out var evo);

			svo.Cost = 0;
			var qs = Array.ConvertAll(new bool[mod], _ => new Queue<Vertex<T>>());
			qs[0].Enqueue(svo);
			var qc = 1;

			for (long c = 0; qc > 0; ++c)
			{
				var q = qs[c % mod];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					--qc;
					if (v == evo) return;
					if (v.Cost < c) continue;

					foreach (var (nv, cost) in v.Edges)
					{
						var nc = c + cost;
						if (nv.Cost <= nc) continue;
						nv.Cost = nc;
						nv.Previous = v;
						qs[nc % mod].Enqueue(nv);
						++qc;
					}
				}
			}
		}

		public T[] GetPathVertexes(T ev)
		{
			var path = new Stack<T>();
			for (var v = Vertexes[ev]; v != null; v = v.Previous)
				path.Push(v.Id);
			return path.ToArray();
		}

		public (T, T)[] GetPathEdges(T ev)
		{
			var path = new Stack<(T, T)>();
			for (var v = Vertexes[ev]; v.Previous != null; v = v.Previous)
				path.Push((v.Previous.Id, v.Id));
			return path.ToArray();
		}
	}
}
