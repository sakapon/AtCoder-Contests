using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int.Others.DirectedGraphEx101
{
	[System.Diagnostics.DebuggerDisplay(@"VertexesCount = {VertexesCount}")]
	public abstract class UnweightedGraph
	{
		protected readonly int n;
		public int VertexesCount => n;
		public abstract List<int> GetEdges(int v);
		protected UnweightedGraph(int n) { this.n = n; }
	}

	public class ListUnweightedGraph : UnweightedGraph
	{
		protected readonly List<int>[] map;
		public List<int>[] AdjacencyList => map;
		public override List<int> GetEdges(int v) => map[v];

		public ListUnweightedGraph(List<int>[] map) : base(map.Length) { this.map = map; }
		public ListUnweightedGraph(int n) : base(n)
		{
			map = Array.ConvertAll(new bool[n], _ => new List<int>());
		}
		public ListUnweightedGraph(int n, IEnumerable<(int from, int to)> edges, bool twoWay) : this(n)
		{
			foreach (var (from, to) in edges) AddEdge(from, to, twoWay);
		}

		public void AddEdge(int from, int to, bool twoWay)
		{
			map[from].Add(to);
			if (twoWay) map[to].Add(from);
		}
	}

	public static class DirectedGraphEx
	{
		// 閉路検査としても利用できます。O(n + m)
		// 連結性、多重性およびコストの有無を問いません。
		// 連結成分の順になるとは限りません。
		// 連結成分ごとに出力するには、先に Union-Find などで分割してから呼び出します。
		// 閉路があるとき、null。
		// DAG であるとき、ソートされた頂点集合。
		public static int[] TopologicalSort(this UnweightedGraph graph)
		{
			var n = graph.VertexesCount;
			var indeg = new int[n];
			for (int v = 0; v < n; ++v)
				foreach (var nv in graph.GetEdges(v)) ++indeg[nv];

			var r = new List<int>();
			var q = new Queue<int>();
			for (int v = 0; v < n; ++v)
				if (indeg[v] == 0) q.Enqueue(v);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				r.Add(v);
				foreach (var nv in graph.GetEdges(v))
					if (--indeg[nv] == 0) q.Enqueue(nv);
			}

			if (r.Count < n) return null;
			return r.ToArray();
		}
	}
}
