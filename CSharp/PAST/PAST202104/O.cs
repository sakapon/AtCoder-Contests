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

		var (tree, es2) = GetUndirectedTree(n + 1, es);
		var lca = new DoublingLca(n + 1, 1, tree);

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

class DoublingLca
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

	List<int>[] map;
	int[] depths;
	// j から 2^i 番目の親
	int[][] parents;

	public DoublingLca(int n, int root, List<int>[] _map)
	{
		map = _map;
		depths = new int[n];
		parents = new int[n][];

		var logn = 0;
		while ((1 << logn) <= n) logn++;
		parents = new int[logn + 1][];
		parents[0] = Array.ConvertAll(new bool[n], _ => root);
		Dfs(root, -1);

		for (int i = 0; i < logn; ++i)
		{
			parents[i + 1] = new int[n];
			for (int j = 0; j < n; ++j)
				parents[i + 1][j] = parents[i][parents[i][j]];
		}

		void Dfs(int v, int pv)
		{
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				depths[nv] = depths[v] + 1;
				parents[0][nv] = v;
				Dfs(nv, v);
			}
		}
	}

	public DoublingLca(int n, int root, int[][] es) : this(n, root, ToMap(n, es, false)) { }

	public int GetAncestor(int v, int depth)
	{
		var delta = depths[v] - depth;
		if (delta == 0) return v;
		if (delta < 0) throw new InvalidOperationException();

		for (int i = 0; ; ++i, delta >>= 1)
			if (delta == 1) return GetAncestor(parents[i][v], depth);
	}

	public int GetLca(int v1, int v2)
	{
		if (depths[v1] < depths[v2])
			v2 = GetAncestor(v2, depths[v1]);
		else if (depths[v1] > depths[v2])
			v1 = GetAncestor(v1, depths[v2]);

		if (v1 == v2) return v1;
		if (parents[0][v1] == parents[0][v2]) return parents[0][v1];
		for (int i = 1; ; ++i)
			if (parents[i][v1] == parents[i][v2]) return GetLca(parents[i - 1][v1], parents[i - 1][v2]);
	}

	public int GetDistance(int v1, int v2)
	{
		var lca = GetLca(v1, v2);
		return depths[v1] + depths[v2] - 2 * depths[lca];
	}
}
