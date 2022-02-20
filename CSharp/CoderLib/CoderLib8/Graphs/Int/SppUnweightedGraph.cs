using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc238/tasks/abc238_e
namespace CoderLib8.Graphs.Int
{
	public class SppUnweightedGraph
	{
		static readonly int[] EmptyVertexes = new int[0];

		public int VertexesCount { get; }
		// Map[v] が null である可能性があります。
		List<int>[] Map;

		public SppUnweightedGraph(int n)
		{
			VertexesCount = n;
			Map = new List<int>[n];
		}

		public int[][] GetMap() => Array.ConvertAll(Map, l => l?.ToArray() ?? EmptyVertexes);

		public void AddEdge(int[] e, bool directed) => AddEdge(e[0], e[1], directed);
		public void AddEdge(int from, int to, bool directed)
		{
			if (Map[from] == null) Map[from] = new List<int>();
			Map[from].Add(to);

			if (directed) return;
			if (Map[to] == null) Map[to] = new List<int>();
			Map[to].Add(from);
		}

		public void AddEdges(IEnumerable<int[]> es, bool directed)
		{
			foreach (var e in es) AddEdge(e[0], e[1], directed);
		}

		public bool[] Dfs(int sv, int ev = -1) => Dfs(VertexesCount, v => Map[v]?.ToArray() ?? EmptyVertexes, sv, ev);

		// 終点を指定しないときは、-1 を指定します。
		public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			var q = new Stack<int>();
			u[sv] = true;
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var nv in nexts(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					if (nv == ev) return u;
					q.Push(nv);
				}
			}
			return u;
		}

		public static bool[] Dfs2(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			u[sv] = true;
			_Dfs(sv);
			return u;

			bool _Dfs(int v)
			{
				foreach (var nv in nexts(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					if (nv == ev) return true;
					if (_Dfs(nv)) return true;
				}
				return false;
			}
		}
	}
}
