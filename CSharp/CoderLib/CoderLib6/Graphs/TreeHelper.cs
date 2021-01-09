using System;
using System.Collections.Generic;
using CoderLib6.Trees;

namespace CoderLib6.Graphs
{
	// Test: https://atcoder.jp/contests/arc108/tasks/arc108_c
	static class TreeHelper
	{
		// 連結な無向グラフから無向木を取り出します。
		static int[][] GetUndirectedTree(int n, int[][] ues)
		{
			var uf = new UF(n);
			var res = new List<int[]>();

			foreach (var e in ues)
			{
				if (uf.AreUnited(e[0], e[1])) continue;
				uf.Unite(e[0], e[1]);
				res.Add(e);
			}
			return res.ToArray();
		}

		// 根を指定して、連結なグラフから有向木を取り出します。
		static int[][] GetTree(int n, int[][] es, bool directed, int rv)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var res = new List<int[]>();
			var u = new bool[n];

			Action<int> Dfs = null;
			Dfs = v =>
			{
				u[v] = true;
				foreach (var e in map[v])
				{
					if (u[e[1]]) continue;
					res.Add(e);
					Dfs(e[1]);
				}
			};

			Dfs(rv);
			return res.ToArray();
		}

		// 実験中の機能です。
		static void TreeTour(int n, int[][] es, bool directed, int rv, Action<int[]> going = null, Action<int[]> backing = null)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			Action<int[]> Dfs = null;
			Dfs = pe =>
			{
				foreach (var e in map[pe[1]])
				{
					if (e[1] == pe[0]) continue;
					going?.Invoke(e);
					Dfs(e);
					backing?.Invoke(e);
				}
			};
			Dfs(new[] { -1, rv, -1 });
		}
	}
}
