using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Int.UnweightedGraph401
{
	public static class UnweightedGraphEx2
	{
		// 再帰を使わない実装です。
		public static Vertex[] ConnectivityByDFS2(this UnweightedGraph graph, int sv, int ev = -1)
		{
			var vs = new Vertex[graph.VertexesCount];
			for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

			vs[sv].Cost = 0;
			var q = new Stack<int>();
			var es = new Queue<int>[graph.VertexesCount];
			q.Push(sv);
			es[sv] = new Queue<int>(graph.GetEdges(sv));

			while (q.Count > 0)
			{
				var v = q.Peek();
				if (v == ev) return vs;
				if (es[v].Count == 0)
				{
					q.Pop();
					continue;
				}

				var nv = es[v].Dequeue();
				var vo = vs[v];
				var nvo = vs[nv];
				if (nvo.Cost != long.MaxValue) continue;
				nvo.Cost = vo.Cost + 1;
				nvo.Parent = vo;
				q.Push(nv);
				es[nv] = new Queue<int>(graph.GetEdges(nv));
			}
			return vs;
		}
	}
}
