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

		// unweighted
		static int[][] EdgesToMatrix1(int n, int[][] es, bool directed)
		{
			var m = Array.ConvertAll(new bool[n], _ => new int[n]);
			//for (int i = 0; i < n; ++i) m[i][i] = 1;
			foreach (var e in es)
			{
				m[e[0]][e[1]] = 1;
				if (!directed) m[e[1]][e[0]] = 1;
			}
			return m;
		}

		// weighted
		static long[][] EdgesToMatrix2(int n, int[][] es, bool directed)
		{
			var m = Array.ConvertAll(new bool[n], _ => Array.ConvertAll(new bool[n], __ => long.MaxValue));
			for (int i = 0; i < n; ++i) m[i][i] = 0;
			foreach (var e in es)
			{
				m[e[0]][e[1]] = Math.Min(m[e[0]][e[1]], e[2]);
				if (!directed) m[e[1]][e[0]] = Math.Min(m[e[1]][e[0]], e[2]);
			}
			return m;
		}

		static int[][] MapToEdges(List<int[]>[] map)
		{
			var r = new List<int[]>();
			foreach (var es in map)
				foreach (var e in es)
					r.Add(e);
			return r.ToArray();
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
