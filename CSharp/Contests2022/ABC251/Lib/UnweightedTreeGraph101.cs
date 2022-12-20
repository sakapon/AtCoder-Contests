using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc251/tasks/abc251_f

namespace CoderLib8.Graphs.Specialized.Int.UnweightedTreeGraph101
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges?.Count ?? -1} edges, Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<int> Edges { get; set; }
		public long Cost { get; set; } = -1;
		public bool IsConnected => Cost != -1;
		public Vertex Parent { get; set; }
		public Vertex(int id) { Id = id; }
	}

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class UnweightedTreeGraph
	{
		public List<int>[] Map { get; }
		public int VertexesCount => Map.Length;

		public UnweightedTreeGraph(int vertexesCount)
		{
			Map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}
		public UnweightedTreeGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			Map[from].Add(to);
			if (twoWay) Map[to].Add(from);
		}

		public Vertex[] DFSTree(int root)
		{
			var vs = new Vertex[VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			vs[root].Cost = 0;
			DFS(root);
			return vs;

			void DFS(int v)
			{
				var vo = vs[v];
				vo.Edges = Map[v];

				foreach (var nv in vo.Edges)
				{
					var nvo = vs[nv];
					if (nvo.Cost != -1) continue;
					nvo.Cost = vo.Cost + 1;
					nvo.Parent = vo;
					DFS(nv);
				}
			}
		}

		public Vertex[] BFSTree(int root)
		{
			var vs = new Vertex[VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			vs[root].Cost = 0;
			var q = new Queue<int>();
			q.Enqueue(root);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var vo = vs[v];
				vo.Edges = Map[v];

				foreach (var nv in vo.Edges)
				{
					var nvo = vs[nv];
					if (nvo.Cost != -1) continue;
					nvo.Cost = vo.Cost + 1;
					nvo.Parent = vo;
					q.Enqueue(nv);
				}
			}
			return vs;
		}
	}
}
