using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var map = new int[n][];
		var siblings = Array.ConvertAll(map, _ => -1);
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			map[a[0]] = a.Skip(1).Where(v => v != -1).ToArray();

			if (a[1] != -1 && a[2] != -1)
			{
				siblings[a[1]] = a[2];
				siblings[a[2]] = a[1];
			}
		}

		var root = Enumerable.Range(0, n).Except(map.SelectMany(vs => vs)).Single();
		var tree = new UnweightedTree(n, root, map);

		var heights = new int[n];
		Func<int, int> GetHeight = null;
		GetHeight = v =>
		{
			if (map[v].Length == 0) return 0;
			return heights[v] = map[v].Max(GetHeight) + 1;
		};
		GetHeight(root);

		for (int v = 0; v < n; v++)
		{
			Console.Write($"node {v}: ");
			Console.Write($"parent = {tree.Parents[v]}, ");
			Console.Write($"sibling = {siblings[v]}, ");
			Console.Write($"degree = {map[v].Length}, ");
			Console.Write($"depth = {tree.Depths[v]}, ");
			Console.Write($"height = {heights[v]}, ");
			Console.WriteLine(v == root ? "root" : map[v].Length > 0 ? "internal node" : "leaf");
		}
	}
}
