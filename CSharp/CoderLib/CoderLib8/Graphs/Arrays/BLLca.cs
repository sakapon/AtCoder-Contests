using System;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://codeforces.com/contest/1304/problem/E
	public class BLLca
	{
		Tree tree;
		// j から 2^i 番目の親
		int[][] parents;

		public BLLca(Tree tree)
		{
			this.tree = tree;
			var n = tree.Count;

			var logn = 0;
			while ((1 << logn) <= n) logn++;
			parents = new int[logn + 1][];
			parents[0] = Array.ConvertAll(tree.Parents, v => v == -1 ? tree.Root : v);

			for (int i = 0; i < logn; ++i)
			{
				parents[i + 1] = new int[n];
				for (int j = 0; j < n; ++j)
					parents[i + 1][j] = parents[i][parents[i][j]];
			}
		}

		public int GetAncestor(int v, int depth)
		{
			var delta = tree.Depths[v] - depth;
			if (delta < 0) throw new ArgumentOutOfRangeException(nameof(depth));
			if (delta == 0) return v;

			for (int i = 0; ; ++i, delta >>= 1)
				if (delta == 1) return GetAncestor(parents[i][v], depth);
		}

		public int GetLca(int v1, int v2)
		{
			var depth = Math.Min(tree.Depths[v1], tree.Depths[v2]);
			v1 = GetAncestor(v1, depth);
			v2 = GetAncestor(v2, depth);

			if (v1 == v2) return v1;
			for (int i = 0; ; ++i)
				if (parents[i + 1][v1] == parents[i + 1][v2]) return GetLca(parents[i][v1], parents[i][v2]);
		}

		public int GetDistance(int v1, int v2)
		{
			var lca = GetLca(v1, v2);
			return tree.Depths[v1] + tree.Depths[v2] - 2 * tree.Depths[lca];
		}
	}
}
