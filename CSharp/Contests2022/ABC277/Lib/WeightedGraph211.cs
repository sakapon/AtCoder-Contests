using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Int.WeightedGraph211
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<(int to, long cost)> Edges { get; } = new List<(int, long)>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public int Previous { get; set; } = -1;
		public Vertex(int id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class WeightedGraph
	{
		public Vertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public Vertex this[int v] => Vertexes[v];

		public WeightedGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public WeightedGraph(int vertexesCount, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public WeightedGraph(int vertexesCount, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public void AddEdge(int from, int to, bool twoWay, long cost)
		{
			Vertexes[from].Edges.Add((to, cost));
			if (twoWay) Vertexes[to].Edges.Add((from, cost));
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes)
			{
				v.Cost = long.MaxValue;
				v.Previous = -1;
			}
		}

		public void Dijkstra(int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var q = new SortedSet<(long, int)> { (0, sv) };

			while (q.Count > 0)
			{
				var (c, v) = q.Min;
				q.Remove((c, v));
				if (v == ev) return;
				var vo = Vertexes[v];

				foreach (var (nv, cost) in vo.Edges)
				{
					var nvo = Vertexes[nv];
					var nc = c + cost;
					if (nvo.Cost <= nc) continue;
					if (nvo.Cost != long.MaxValue) q.Remove((nvo.Cost, nv));
					q.Add((nc, nv));
					nvo.Cost = nc;
					nvo.Previous = v;
				}
			}
		}

		// Dijkstra 法の特別な場合です。
		public void ShortestByModBFS(int mod, int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var qs = Array.ConvertAll(new bool[mod], _ => new Queue<int>());
			qs[0].Enqueue(sv);
			var qc = 1;

			for (long c = 0; qc > 0; ++c)
			{
				var q = qs[c % mod];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					--qc;
					if (v == ev) return;
					var vo = Vertexes[v];
					if (vo.Cost < c) continue;

					foreach (var (nv, cost) in vo.Edges)
					{
						var nvo = Vertexes[nv];
						var nc = c + cost;
						if (nvo.Cost <= nc) continue;
						nvo.Cost = nc;
						nvo.Previous = v;
						qs[nc % mod].Enqueue(nv);
						++qc;
					}
				}
			}
		}

		public int[] GetPathVertexes(int ev)
		{
			var path = new Stack<int>();
			for (var v = ev; v != -1; v = Vertexes[v].Previous)
				path.Push(v);
			return path.ToArray();
		}

		public (int, int)[] GetPathEdges(int ev)
		{
			var path = new Stack<(int, int)>();
			for (int v = ev, pv = Vertexes[v].Previous; pv != -1; v = pv, pv = Vertexes[v].Previous)
				path.Push((pv, v));
			return path.ToArray();
		}
	}
}
