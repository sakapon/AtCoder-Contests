using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var (treeEdges, es2) = GetUndirectedTree(n + 1, es);
		var tree = new Tree(n + 1, 1, treeEdges);
		var lca = new BLLca(tree);

		var vs2 = es2.SelectMany(e => e).Distinct().ToArray();
		var vmap = Enumerable.Range(0, vs2.Length).ToDictionary(i => vs2[i], i => i);

		var wfes = new List<int[]>();
		for (int i = 0; i < vs2.Length; i++)
			for (int j = 0; j < vs2.Length; j++)
				wfes.Add(new[] { i, j, lca.GetDistance(vs2[i], vs2[j]) });
		foreach (var e in es2)
			wfes.Add(new[] { vmap[e[0]], vmap[e[1]], 1 });
		var wfd = WarshallFloyd(vs2.Length, wfes.ToArray(), false).Item1;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (u, v) in qs)
		{
			var r = lca.GetDistance(u, v);

			for (int i = 0; i < vs2.Length; i++)
				for (int j = 0; j < vs2.Length; j++)
				{
					var d = lca.GetDistance(u, vs2[i]) + (int)wfd[i][j] + lca.GetDistance(vs2[j], v);
					r = Math.Min(r, d);
				}
			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}

	// Partitioning
	static (int[][], int[][]) GetUndirectedTree(int n, int[][] ues)
	{
		var uf = new UF(n);
		var res = new List<int[]>();
		var res2 = new List<int[]>();

		foreach (var e in ues)
		{
			if (uf.AreUnited(e[0], e[1]))
			{
				res2.Add(e);
			}
			else
			{
				uf.Unite(e[0], e[1]);
				res.Add(e);
			}
		}
		return (res.ToArray(), res2.ToArray());
	}

	public static Tuple<long[][], int[][]> WarshallFloyd(int n, int[][] es, bool directed)
	{
		var cs = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => long.MaxValue));
		var inters = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => -1));
		for (int i = 0; i < n; ++i) cs[i][i] = 0;

		foreach (var e in es)
		{
			cs[e[0]][e[1]] = Math.Min(cs[e[0]][e[1]], e[2]);
			if (!directed) cs[e[1]][e[0]] = Math.Min(cs[e[1]][e[0]], e[2]);
		}

		for (int k = 0; k < n; ++k)
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
				{
					if (cs[i][k] == long.MaxValue || cs[k][j] == long.MaxValue) continue;
					var nc = cs[i][k] + cs[k][j];
					if (cs[i][j] <= nc) continue;
					cs[i][j] = nc;
					inters[i][j] = k;
				}
		for (int i = 0; i < n; ++i) if (cs[i][i] < 0) return Tuple.Create<long[][], int[][]>(null, null);
		return Tuple.Create(cs, inters);
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

	public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
	public Tree(int n, int root, List<int>[] map)
	{
		Count = n;
		Root = root;
		Map = map;
		Depths = Array.ConvertAll(Map, _ => -1);
		Parents = Array.ConvertAll(Map, _ => -1);

		Depths[root] = 0;
		Dfs(root, -1);

		void Dfs(int v, int pv)
		{
			foreach (var nv in Map[v])
			{
				if (nv == pv) continue;
				Depths[nv] = Depths[v] + 1;
				Parents[nv] = v;
				Dfs(nv, v);
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
