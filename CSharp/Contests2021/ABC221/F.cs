using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var tree = new Tree(n + 1, 1, es);
		var (mv1, _) = GetFarthest(tree);

		var map = tree.Map;
		var tree1 = new Tree(n + 1, mv1, map);
		var (mv2, d) = GetFarthest(tree1);

		if (d % 2 == 0)
		{
			var d2 = d / 2;
			var center = mv2;
			while (tree1.Depths[center] != d2)
				center = tree1.Parents[center];

			var prod = 1L;
			var sum = 0L;
			var count = 0;

			foreach (var nv in map[center])
			{
				count = 0;
				Dfs(nv, center, 1);

				prod *= count + 1;
				prod %= M;
				sum += count;
			}

			return (prod - sum - 1 + M) % M;

			void Dfs(int v, int pv, int depth)
			{
				if (depth == d2) count++;

				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					Dfs(nv, v, depth + 1);
				}
			}
		}
		else
		{
			var tree2 = new Tree(n + 1, mv2, map);
			var m1 = tree1.Depths.LongCount(x => x == d);
			var m2 = tree2.Depths.LongCount(x => x == d);

			return m1 * m2 % M;
		}
	}

	const long M = 998244353;

	public static (int, int) GetFarthest(Tree tree)
	{
		var (mv, md) = (-1, -1);
		for (int v = 0; v < tree.Count; v++)
		{
			var d = tree.Depths[v];
			if (md < d) (mv, md) = (v, d);
		}
		return (mv, md);
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
