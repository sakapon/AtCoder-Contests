using System;
using System.Collections.Generic;

namespace YJLib8.Graphs.Core.IntEdges
{
	[System.Diagnostics.DebuggerDisplay(@"\{Vertex {Id} : Cost = {Cost}\}")]
	public class Vertex
	{
		public int Id { get; }
		public long Cost { get; set; } = long.MaxValue;
		public bool IsConnected => Cost != long.MaxValue;
		public Vertex Parent { get; set; }
		public Edge ParentEdge { get; set; }
		public Vertex(int id) { Id = id; }

		public int[] GetPathVertexes()
		{
			var path = new Stack<int>();
			for (var v = this; v != null; v = v.Parent)
			{
				path.Push(v.Id);
				if (v.Parent == this) break;
			}
			return path.ToArray();
		}

		public int[] GetPathEdges()
		{
			var path = new Stack<int>();
			for (var v = this; v.Parent != null; v = v.Parent)
			{
				path.Push(v.ParentEdge.Id);
				if (v.Parent == this) break;
			}
			return path.ToArray();
		}
	}

	public static class EdgeGraphEx
	{
		public static Vertex DetectCycle(this EdgeGraph graph)
		{
			var vs = new Vertex[graph.VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			for (int sv = 0; sv < vs.Length; sv++)
			{
				if (vs[sv].IsConnected) continue;
				vs[sv].Cost = 0;
				var r = DFS(sv, -1);
				if (r != null)
				{
					return r;
				}
			}
			return null;

			Vertex DFS(int v, int peid)
			{
				var vo = vs[v];

				foreach (var e in graph.GetEdges(v))
				{
					if (e.Id == peid) continue;
					var nvo = vs[e.To];
					if (nvo.Cost != long.MaxValue)
					{
						nvo.Parent = vo;
						nvo.ParentEdge = e;
						return nvo;
					}
					nvo.Cost = vo.Cost + 1;
					nvo.Parent = vo;
					nvo.ParentEdge = e;
					var r = DFS(e.To, e.Id);
					if (r != null) return r;
				}
				return null;
			}
		}
	}
}
