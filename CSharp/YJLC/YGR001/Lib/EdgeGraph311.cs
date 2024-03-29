﻿using System;
using System.Collections.Generic;

// 頂点および辺をオブジェクトとして扱います。
namespace CoderLib8.Graphs.SPPs.Int.EdgeGraph311
{
	[System.Diagnostics.DebuggerDisplay(@"\{Vertex {Id} : {Edges.Count} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<Edge> Edges { get; } = new List<Edge>();
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Edge Previous { get; set; }
		public Vertex(int id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"\{Edge {Id} : {From.Id} --> {To.Id}, Cost = {Cost}\}")]
	public class Edge
	{
		public int Id { get; }
		public Vertex From { get; }
		public Vertex To { get; }
		public long Cost { get; }
		public Edge Reverse { get; private set; }

		public Edge(int id, Vertex from, Vertex to, bool twoWay, long cost = 1)
		{
			Id = id;
			From = from;
			To = to;
			Cost = cost;
			if (twoWay) Reverse = new Edge(id, to, from, false, cost) { Reverse = this };
		}
	}

	[System.Diagnostics.DebuggerDisplay(@"\{{VertexesCount} vertexes, {EdgesCount} edges\}")]
	public class EdgeGraph
	{
		// id -> vertex
		public Vertex[] Vertexes { get; }
		// id -> edge
		public List<Edge> Edges { get; } = new List<Edge>();
		public int VertexesCount => Vertexes.Length;
		public int EdgesCount => Edges.Count;
		public Vertex this[int v] => Vertexes[v];

		public EdgeGraph(int vertexesCount)
		{
			Vertexes = new Vertex[vertexesCount];
			for (int v = 0; v < vertexesCount; ++v) Vertexes[v] = new Vertex(v);
		}
		public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}
		public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to, int cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}
		public EdgeGraph(int vertexesCount, IEnumerable<(int from, int to, long cost)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to, cost) in edges) AddEdge(from, to, twoWay, cost);
		}

		public void AddEdge(int from, int to, bool twoWay, long cost = 1)
		{
			var fv = Vertexes[from];
			var tv = Vertexes[to];

			var e1 = new Edge(Edges.Count, fv, tv, twoWay, cost);
			Edges.Add(e1);
			fv.Edges.Add(e1);
			if (twoWay) tv.Edges.Add(e1.Reverse);
		}

		// 異なる id の辺として登録します。
		public void AddTwoWayDifferentEdges(int from, int to, long cost = 1)
		{
			var fv = Vertexes[from];
			var tv = Vertexes[to];

			var e1 = new Edge(Edges.Count, fv, tv, false, cost);
			Edges.Add(e1);
			fv.Edges.Add(e1);

			var e2 = new Edge(Edges.Count, tv, fv, false, cost);
			Edges.Add(e2);
			tv.Edges.Add(e2);
		}

		public void ClearResult()
		{
			foreach (var v in Vertexes)
			{
				v.Cost = long.MaxValue;
				v.Previous = null;
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

				foreach (var e in vo.Edges)
				{
					var nvo = e.To;
					if (nvo.Cost == 0) continue;
					nvo.Cost = 0;
					nvo.Previous = e;
					if (nvo.Id == ev) return;
					q.Push(nvo.Id);
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

				foreach (var e in vo.Edges)
				{
					var nvo = e.To;
					if (nvo.Cost <= nc) continue;
					nvo.Cost = nc;
					nvo.Previous = e;
					if (nvo.Id == ev) return;
					q.Enqueue(nvo.Id);
				}
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

				foreach (var e in vo.Edges)
				{
					var nvo = e.To;
					var nc = c + e.Cost;
					if (nvo.Cost <= nc) continue;
					if (nvo.Cost != long.MaxValue) q.Remove((nvo.Cost, nvo.Id));
					q.Add((nc, nvo.Id));
					nvo.Cost = nc;
					nvo.Previous = e;
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

					foreach (var e in vo.Edges)
					{
						var nvo = e.To;
						var nc = c + e.Cost;
						if (nvo.Cost <= nc) continue;
						nvo.Cost = nc;
						nvo.Previous = e;
						qs[nc % mod].Enqueue(nvo.Id);
						++qc;
					}
				}
			}
		}

		public Vertex[] GetPathVertexes(int ev)
		{
			var path = new Stack<Vertex>();
			for (var v = Vertexes[ev]; v != null; v = v.Previous?.From)
				path.Push(v);
			return path.ToArray();
		}

		public Edge[] GetPathEdges(int ev)
		{
			var path = new Stack<Edge>();
			for (var e = Vertexes[ev].Previous; e != null; e = e.From.Previous)
				path.Push(e);
			return path.ToArray();
		}
	}
}
