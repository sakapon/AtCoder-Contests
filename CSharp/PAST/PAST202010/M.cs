using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var tree = new Tree(n + 1, 1, es);
		var colors = new int[n - 1];

		// 親への辺
		var eis = new int[n + 1];
		for (int ei = 0; ei < n - 1; ei++)
		{
			var e = es[ei];
			eis[tree.Parents[e[0]] == e[1] ? e[0] : e[1]] = ei;
		}

		// 連結成分の根
		var uf = new UF<int>(n + 1,
			(v1, v2) => tree.Depths[v1] < tree.Depths[v2] ? v1 : v2,
			Enumerable.Range(0, n + 1).ToArray());

		Array.Reverse(qs);
		foreach (var q in qs)
		{
			var (u, v, c) = (q[0], q[1], q[2]);
			u = uf.GetValue(u);
			v = uf.GetValue(v);

			while (u != v)
			{
				if (tree.Depths[u] < tree.Depths[v])
				{
					v = Connect(v);
				}
				else
				{
					u = Connect(u);
				}
			}

			int Connect(int v)
			{
				colors[eis[v]] = c;
				uf.Unite(v, tree.Parents[v]);
				return uf.GetValue(v);
			}
		}

		return string.Join("\n", colors);
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
