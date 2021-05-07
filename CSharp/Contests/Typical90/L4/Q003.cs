using System;
using System.Collections.Generic;
using System.Linq;

class Q003
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		return Diameter(n + 1, 1, es) + 1;
	}

	static int Diameter(int n, int root, int[][] ues)
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

	public List<int>[] Map { get; }
	public int[] Depths { get; }
	public int[] Parents { get; }

	public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
	public Tree(int n, int root, List<int>[] map)
	{
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
