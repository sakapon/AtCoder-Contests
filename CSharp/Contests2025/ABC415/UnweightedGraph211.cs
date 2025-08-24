using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Int.UnweightedGraph211
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<int> Edges { get; } = new List<int>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public int Previous { get; set; } = -1;
		public Vertex(int id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class UnweightedGraph
	{
		public Vertex[] Vertexes { get; }
		public int VertexesCount => Vertexes.Length;
		public Vertex this[int v] => Vertexes[v];

		public UnweightedGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public UnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			Vertexes[from].Edges.Add(to);
			if (twoWay) Vertexes[to].Edges.Add(from);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes)
			{
				v.Cost = long.MaxValue;
				v.Previous = -1;
			}
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS または Union-Find を利用します。
		public void ConnectivityByDFS(int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var q = new Stack<int>();
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				var vo = Vertexes[v];

				foreach (var nv in vo.Edges)
				{
					var nvo = Vertexes[nv];
					if (nvo.Cost == 0) continue;
					nvo.Cost = 0;
					nvo.Previous = v;
					if (nv == ev) return;
					q.Push(nv);
				}
			}
		}

		public void ShortestByBFS(int sv, int ev = -1)
		{
			Vertexes[sv].Cost = 0;
			var q = new Queue<int>();
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var vo = Vertexes[v];
				var nc = vo.Cost + 1;

				foreach (var nv in vo.Edges)
				{
					var nvo = Vertexes[nv];
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Previous = v;
					if (nv == ev) return;
					q.Enqueue(nv);
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
