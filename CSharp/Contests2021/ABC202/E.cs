using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 2; i <= n; i++)
		{
			map[p[i - 2]].Add(i);
		}

		var tree = new Tree(n + 1, 1, map);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (u, d) in qs)
		{
			if (tree.Depths[u] > d)
			{
				Console.WriteLine(0);
			}
			else
			{
				var tours = tree.TourMap[u];
				var d_tours = tree.DepthTourMap[d];
				var m = d_tours.Count;
				var i1 = First(0, m, x => d_tours[x] >= tours[0]);
				var i2 = First(0, m, x => d_tours[x] > tours[^1]);
				Console.WriteLine(i2 - i1);
			}
		}
		Console.Out.Flush();
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
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

	public List<int>[] DepthTourMap { get; }

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

		DepthTourMap = Array.ConvertAll(Map, _ => new List<int>());

		Depths[root] = 0;
		Dfs(root, -1);

		Tour = tour.ToArray();

		void Dfs(int v, int pv)
		{
			TourMap[v].Add(tour.Count);
			DepthTourMap[Depths[v]].Add(tour.Count);
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
