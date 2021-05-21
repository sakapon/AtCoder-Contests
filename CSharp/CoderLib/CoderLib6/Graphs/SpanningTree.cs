using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

namespace CoderLib6.Graphs
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/2/GRL_2_A
	static class SpanningTree
	{
		// n: 頂点の個数
		// 結果は undirected
		static int[][] Kruskal(int n, int[][] es)
		{
			var uf = new UF(n);
			var minEdges = new List<int[]>();

			foreach (var e in es.OrderBy(e => e[2]))
			{
				if (uf.AreUnited(e[0], e[1])) continue;
				uf.Unite(e[0], e[1]);
				minEdges.Add(e);
			}
			return minEdges.ToArray();
		}

		// n: 頂点の個数
		// 結果は directed
		static int[][] Prim(int n, int[][] es, int sv)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var u = new bool[n];
			var pq = PQ<int[]>.Create(e => e[2]);
			u[sv] = true;
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
