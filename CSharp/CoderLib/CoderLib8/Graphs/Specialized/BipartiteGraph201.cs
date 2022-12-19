using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc282/tasks/abc282_d

// 連結成分ごとに探索するため、非連結でも可能です。
// Root は連結成分の id を表します。
namespace CoderLib8.Graphs.Specialized.Int.BipartiteGraph201
{
	[System.Diagnostics.DebuggerDisplay(@"\{{Id}: {Edges?.Count ?? -1} edges, Color = {Color}\}")]
	public class Vertex
	{
		public int Id { get; }
		public List<int> Edges { get; set; }
		public int Color { get; set; } = -1;
		public Vertex Parent { get; set; }
		public Vertex Root { get; set; }
		public Vertex(int id) { Id = id; }
	}

	public static class BipartiteGraphEx
	{
		public static Vertex[] BFSForest(this UnweightedGraph graph)
		{
			var vs = new Vertex[graph.VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			var q = new Queue<int>();

			for (int sv = 0; sv < vs.Length; ++sv)
			{
				var svo = vs[sv];
				if (svo.Color != -1) continue;
				svo.Color = 0;
				svo.Root = svo;
				q.Enqueue(sv);

				while (q.Count > 0)
				{
					var v = q.Dequeue();
					var vo = vs[v];
					vo.Edges = graph.GetEdges(v);

					foreach (var nv in vo.Edges)
					{
						var nvo = vs[nv];
						if (nvo.Color != -1)
						{
							if (nvo.Color == vo.Color) return null;
							continue;
						}
						nvo.Color = 1 - vo.Color;
						nvo.Parent = vo;
						nvo.Root = svo;
						q.Enqueue(nv);
					}
				}
			}
			return vs;
		}
	}
}
