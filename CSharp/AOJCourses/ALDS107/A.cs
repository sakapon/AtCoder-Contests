using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var map = new int[n][];
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			map[a[0]] = a.Skip(2).ToArray();
		}

		var root = Enumerable.Range(0, n).Except(map.SelectMany(vs => vs)).Single();
		var tree = new UnweightedTree(n, root, map);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int v = 0; v < n; v++)
		{
			Console.Write($"node {v}: ");
			Console.Write($"parent = {tree.Parents[v]}, ");
			Console.Write($"depth = {tree.Depths[v]}, ");
			Console.Write(v == root ? "root" : map[v].Length > 0 ? "internal node" : "leaf");
			Console.Write($", [");
			Console.Write(string.Join(", ", map[v]));
			Console.WriteLine("]");
		}
		Console.Out.Flush();
	}
}

public class UnweightedTree
{
	public int Count { get; }
	public int Root { get; }
	public int[][] Map { get; }
	public int[] Depths { get; }
	public int[] Parents { get; }

	// この Euler Tour では方向を記録しません。
	// order -> vertex
	public int[] Tour { get; }
	// vertex -> orders
	public List<int>[] TourMap { get; }

	public UnweightedTree(int n, int root, int[][] map)
	{
		Count = n;
		Root = root;
		Map = map;
		Depths = Array.ConvertAll(Map, _ => -1);
		Parents = Array.ConvertAll(Map, _ => -1);

		TourMap = Array.ConvertAll(Map, _ => new List<int>());

		Depths[root] = 0;
		Dfs(root, -1);

		Tour = tour.ToArray();
	}

	List<int> tour = new List<int>();
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
