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
