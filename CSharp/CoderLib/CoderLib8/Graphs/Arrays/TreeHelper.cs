using System;
using System.Linq;

namespace CoderLib8.Graphs.Arrays
{
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
	}
}
