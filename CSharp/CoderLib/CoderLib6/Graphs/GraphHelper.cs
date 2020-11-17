using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib6.Graphs
{
	static class GraphHelper
	{
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
