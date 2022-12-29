using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc251/tasks/abc251_f
// SPPs.Int.UnweightedGraph401 と実質同じです。

namespace CoderLib8.Graphs.Specialized.Int.UnweightedTreeGraph201
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public long Cost { get; set; } = -1;
		public bool IsConnected => Cost != -1;
		public Vertex Parent { get; set; }
		public Vertex(int id) { Id = id; }
	}

	public static class UnweightedTreeGraphEx
	{
		public static Vertex[] DFSTree(this UnweightedGraph graph, int root)
		{
			var vs = new Vertex[graph.VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			vs[root].Cost = 0;
			DFS(root);
			return vs;

			void DFS(int v)
			{
				var vo = vs[v];

				foreach (var nv in graph.GetEdges(v))
				{
					var nvo = vs[nv];
					if (nvo.Cost != -1) continue;
					nvo.Cost = vo.Cost + 1;
					nvo.Parent = vo;
					DFS(nv);
				}
			}
		}

		public static Vertex[] BFSTree(this UnweightedGraph graph, int root)
		{
			var vs = new Vertex[graph.VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			vs[root].Cost = 0;
			var q = new Queue<int>();
			q.Enqueue(root);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var vo = vs[v];

				foreach (var nv in graph.GetEdges(v))
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
