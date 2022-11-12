using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF402;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read2());

		var uf = new UnionFind<int>();

		foreach (var (a, b) in es)
		{
			uf.Union(a, b);
		}

		var gs = uf.ToGroups();
		var g = gs.FirstOrDefault(g => g.Any(node => node.Item == 1));
		if (g == null) return 1;
		return g.Max(node => node.Item);
	}
}
