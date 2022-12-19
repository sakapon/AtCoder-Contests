using System;
using System.Collections.Generic;

// 連結成分ごとに探索するため、非連結でも可能です。
// Root は連結成分の id を表します。
namespace CoderLib8.Graphs.Int.BipartiteGraph101
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

	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public class BipartiteGraph
	{
		public List<int>[] Map { get; }
		public int VertexesCount => Map.Length;

		public BipartiteGraph(int vertexesCount)
		{
			Map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}
		public BipartiteGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			Map[from].Add(to);
			if (twoWay) Map[to].Add(from);
		}

		public Vertex[] BFSForest()
		{
			var vs = new Vertex[VertexesCount];
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
					vo.Edges = Map[v];

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
