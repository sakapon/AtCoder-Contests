using System;
using System.Collections.Generic;

// 頂点 id として整数 [0, n) を使います。
namespace CoderLib8.Graphs.Int
{
	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public abstract class UnweightedGraph
	{
		public int VertexesCount { get; }
		public abstract List<int> GetEdges(int v);
		protected UnweightedGraph(int vertexesCount) { VertexesCount = vertexesCount; }
	}

	public class ListUnweightedGraph : UnweightedGraph
	{
		public List<int>[] Map { get; }
		public override List<int> GetEdges(int v) => Map[v];

		public ListUnweightedGraph(int vertexesCount) : base(vertexesCount)
		{
			Map = Array.ConvertAll(new bool[vertexesCount], _ => new List<int>());
		}
		public ListUnweightedGraph(int vertexesCount, IEnumerable<(int from, int to)> edges, bool twoWay) : this(vertexesCount)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			Map[from].Add(to);
			if (twoWay) Map[to].Add(from);
		}
	}
}
