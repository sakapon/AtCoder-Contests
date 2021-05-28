using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

namespace CoderLib6.Graphs.Arrays
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_A
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/2/GRL_2_A
	// Test: https://atcoder.jp/contests/past201912-open/tasks/past201912_l
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_aw
	public static class SpanningTreeHelper
	{
		// n: 頂点の個数
		// 結果は undirected
		public static int[][] Kruskal(int n, int[][] ues)
		{
			var uf = new UF(n);
			var mes = new List<int[]>();

			foreach (var e in ues.OrderBy(e => e[2]))
			{
				if (uf.AreUnited(e[0], e[1])) continue;
				uf.Unite(e[0], e[1]);
				mes.Add(e);
				// 実際の頂点数に注意。
				// あまり実行速度に影響しないようです。
				//if (mes.Count == n - 1) break;
			}
			return mes.ToArray();
		}

		// n: 頂点の個数
		// 結果は directed
		public static int[][] Prim(int n, int root, int[][] ues) => Prim(n, root, ToMap(n, ues, false));
		public static int[][] Prim(int n, int root, List<int[]>[] map)
		{
			var u = new bool[n];
			var q = PQ<int[]>.Create(e => e[2]);
			u[root] = true;
			q.PushRange(map[root].ToArray());
			var mes = new List<int[]>();

			// 実際の頂点数に注意。
			while (q.Count > 0 && mes.Count < n - 1)
			{
				var e = q.Pop();
				if (u[e[1]]) continue;
				u[e[1]] = true;
				mes.Add(e);
				foreach (var ne in map[e[1]])
					if (ne[1] != e[0])
						q.Push(ne);
			}
			return mes.ToArray();
		}

		static List<int[]>[] ToMap(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(e);
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}
	}
}
