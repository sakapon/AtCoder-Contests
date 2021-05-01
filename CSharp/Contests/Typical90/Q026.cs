using System;
using System.Collections.Generic;
using System.Linq;

class Q026
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var tree = new Tree(n + 1, 1, es);
		var gs = Enumerable.Range(1, n).ToLookup(v => tree.Depths[v] % 2);

		var r = gs[0].Count() >= n / 2 ? gs[0] : gs[1];
		return string.Join(" ", r.Take(n / 2));
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
		Depths = new int[n];
		Parents = new int[n];
		Parents[root] = -1;

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
