using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib6.Graphs
{
	static class GraphHelper
	{
		// weighted
		static List<int[]>[] EdgesToMap(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}

		// 単純
		static int[][] EdgesToMatrix(int n, int[][] es, bool directed)
		{
			var m = Array.ConvertAll(new bool[n], _ => new int[n]);
			foreach (var e in es)
			{
				m[e[0]][e[1]] = e.Length > 2 ? e[2] : 1;
				if (!directed) m[e[1]][e[0]] = e.Length > 2 ? e[2] : 1;
			}
			return m;
		}

		// 注意: オブジェクト参照を再利用しています。
		static int[][] MapToEdges(List<int[]>[] map)
		{
			var r = new List<int[]>();
			foreach (var es in map)
				foreach (var e in es)
					r.Add(e);
			return r.ToArray();
		}

		// 単純
		static int[][] MapToMatrix(List<int[]>[] map)
		{
			var n = map.Length;
			var m = Array.ConvertAll(new bool[n], _ => new int[n]);
			foreach (var es in map)
				foreach (var e in es)
					m[e[0]][e[1]] = e.Length > 2 ? e[2] : 1;
			return m;
		}

		static int[][] MatrixToEdges(int[][] m)
		{
			var n = m.Length;
			var r = new List<int[]>();
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
					if (m[i][j] > 0)
						r.Add(new[] { i, j, m[i][j] });
			return r.ToArray();
		}

		static List<int[]>[] MatrixToMap(int[][] m)
		{
			var n = m.Length;
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
					if (m[i][j] > 0)
						map[i].Add(new[] { i, j, m[i][j] });
			return map;
		}

		// unweighted
		static int[][] ToUndirectedEdges1(int[][] des) => des.Concat(des.Select(e => new[] { e[1], e[0] })).ToArray();
		// weighted
		static int[][] ToUndirectedEdges2(int[][] des) => des.Concat(des.Select(e => new[] { e[1], e[0], e[2] })).ToArray();

		static void ToUndirectedMatrix(int[][] m)
		{
			var n = m.Length;
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
					if (m[i][j] > 0)
						m[j][i] = m[i][j];
		}
	}
}
