using System;
using System.Linq;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_c
	public static class TreeHelper
	{
		public static int Diameter(int n, int root, int[][] ues)
		{
			var tree = new Tree(n, root, ues);

			var (mv, md) = (-1, -1);
			for (int v = 0; v < n; v++)
			{
				var d = tree.Depths[v];
				if (md < d) (mv, md) = (v, d);
			}
			return new Tree(n, mv, tree.Map).Depths.Max();
		}

		public static long WeightedDiameter(int n, int root, int[][] ues)
		{
			var tree = new WeightedTree(n, root, ues);

			var (mv, md) = (-1, -1L);
			for (int v = 0; v < n; v++)
			{
				var d = tree.Costs[v];
				if (md < d) (mv, md) = (v, d);
			}
			return new WeightedTree(n, mv, tree.Map).Depths.Max();
		}
	}
}
