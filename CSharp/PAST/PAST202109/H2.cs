using System;
using System.Collections.Generic;
using System.Linq;

class H2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, x) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = WeightedTree.ToMap(n + 1, es, false);

		for (int i = 1; i <= n; i++)
		{
			var tree = new WeightedTree(n + 1, i, map);
			if (tree.Costs.Contains(x)) return true;
		}

		return false;
	}
}

public class WeightedTree
{
	public static List<int[]>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	public List<int[]>[] Map { get; }
	public int[] Depths { get; }
	public long[] Costs { get; }
	public int[] Parents { get; }

	public WeightedTree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
	public WeightedTree(int n, int root, List<int[]>[] map)
	{
		Map = map;
		Depths = Array.ConvertAll(Map, _ => -1);
		Costs = Array.ConvertAll(Map, _ => -1L);
		Parents = Array.ConvertAll(Map, _ => -1);

		Depths[root] = 0;
		Costs[root] = 0;
		Dfs(root, -1);

		void Dfs(int v, int pv)
		{
			foreach (var e in Map[v])
			{
				var nv = e[1];
				if (nv == pv) continue;
				Depths[nv] = Depths[v] + 1;
				Costs[nv] = Costs[v] + e[2];
				Parents[nv] = v;
				Dfs(nv, v);
			}
		}
	}
}
