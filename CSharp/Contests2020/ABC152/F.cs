using System;
using System.Collections.Generic;
using System.Numerics;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var m = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var set = new HashSet<int>();

		var tree = new Tree(n + 1, 1, es);
		var bllca = new BLLca(tree);
		return InclusionExclusion(m, GetCount);

		long GetCount(bool[] b)
		{
			set.Clear();

			for (int j = 0; j < m; j++)
			{
				if (b[j])
				{
					var (u, v) = ps[j];
					var lca = bllca.GetLca(u, v);

					for (int x = u; x != lca; x = tree.Parents[x])
					{
						set.Add(x);
					}
					for (int x = v; x != lca; x = tree.Parents[x])
					{
						set.Add(x);
					}
				}
			}

			return 1L << n - 1 - set.Count;
		}
	}

	public static long InclusionExclusion(int n, Func<bool[], long> getCount)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		var r = 0L;
		for (uint x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;

			var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
			r += sign * getCount(b);
		}
		return r;
	}
}

public class Tree
{
	static List<int>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	public int Count { get; }
	public int Root { get; }
	public List<int>[] Map { get; }
	public int[] Depths { get; }
	public int[] Parents { get; }

	// この Euler Tour では方向を記録しません。
	// order -> vertex
	public int[] Tour { get; }
	// vertex -> orders
	public List<int>[] TourMap { get; }

	public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
	public Tree(int n, int root, List<int>[] map)
	{
		Count = n;
		Root = root;
		Map = map;
		Depths = Array.ConvertAll(Map, _ => -1);
		Parents = Array.ConvertAll(Map, _ => -1);

		var tour = new List<int>();
		TourMap = Array.ConvertAll(Map, _ => new List<int>());

		Depths[root] = 0;
		Dfs(root, -1);

		Tour = tour.ToArray();

		void Dfs(int v, int pv)
		{
			TourMap[v].Add(tour.Count);
			tour.Add(v);

			foreach (var nv in Map[v])
			{
				if (nv == pv) continue;
				Depths[nv] = Depths[v] + 1;
				Parents[nv] = v;
				Dfs(nv, v);

				TourMap[v].Add(tour.Count);
				tour.Add(v);
			}
		}
	}
}

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
