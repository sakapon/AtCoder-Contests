using System;
using System.Collections.Generic;
using CoderLib6.Trees;

namespace CoderLib6.Graphs
{
	static class TreeHelper
	{
		// 連結な無向グラフから木を取り出します。
		// 結果は undirected
		// Test: https://atcoder.jp/contests/arc108/tasks/arc108_c
		static int[][] GetTree(int n, int[][] ues)
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
	}
}
