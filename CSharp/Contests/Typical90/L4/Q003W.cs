using System;
using System.Collections.Generic;
using System.Linq;

class Q003W
{
	static int[] Read() => Array.ConvertAll((Console.ReadLine() + " 1").Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		return WeightedDiameter(n + 1, 1, es) + 1;
	}

	static long WeightedDiameter(int n, int root, int[][] ues)
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

public class WeightedTree
{
	static List<int[]>[] ToMap(int n, int[][] es, bool directed)
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
