using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

namespace CoderLib6.Graphs
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_A
	static class SpanningTree
	{
		// n: 最後の番号
		static int[][] Kruskal(int n, int[][] es)
		{
			var uf = new UF(n + 1);
			var minEdges = new List<int[]>();

			foreach (var e in es.OrderBy(e => e[2]))
			{
				if (uf.AreUnited(e[0], e[1])) continue;
				uf.Unite(e[0], e[1]);
				minEdges.Add(e);
			}
			return minEdges.ToArray();
		}

		// n: 最後の番号
		static int[][] Prim(int n, int sv, int[][] es)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var u = new bool[n + 1];
			u[sv] = true;
			var pq = PQ<int[]>.Create(e => e[2]);
			pq.PushRange(map[sv].ToArray());
			var minEdges = new List<int[]>();

			while (pq.Count > 0 && minEdges.Count < n)
			{
				var e = pq.Pop();
				if (u[e[1]]) continue;
				u[e[1]] = true;
				minEdges.Add(e);
				foreach (var ne in map[e[1]])
					if (ne[1] != e[0])
						pq.Push(ne);
			}
			return minEdges.ToArray();
		}
	}
}
