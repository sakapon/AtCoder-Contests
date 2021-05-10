using System;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://codeforces.com/contest/1304/problem/E
	// Test: https://atcoder.jp/contests/past202104-open/tasks/past202104_o
	public class BLLca
	{
		Tree tree;
		// j から 2^i 番目の親
		int[][] parents;

		public BLLca(Tree tree)
		{
			this.tree = tree;
			var n = tree.Count;

			var ln = 0;
			while ((1 << ln) < n) ++ln;
			parents = new int[ln + 1][];
			parents[0] = Array.ConvertAll(tree.Parents, v => v == -1 ? tree.Root : v);

			for (int i = 0; i < ln; ++i)
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
			return GetLcaForLevel(v1, v2);
		}

		int GetLcaForLevel(int v1, int v2)
		{
			if (v1 == v2) return v1;
			for (int i = 0; ; ++i)
				if (parents[i + 1][v1] == parents[i + 1][v2]) return GetLcaForLevel(parents[i][v1], parents[i][v2]);
		}

		public int GetDistance(int v1, int v2)
		{
			var lca = GetLca(v1, v2);
			return tree.Depths[v1] + tree.Depths[v2] - 2 * tree.Depths[lca];
		}
	}
}
