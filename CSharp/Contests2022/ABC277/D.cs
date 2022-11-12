using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLab.DataTrees.UF502;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		var gs = a.GroupBy(x => x).Select(g => (key: g.Key, sum: g.Sum())).OrderBy(p => p.key).ToArray();

		var uf = new UnionFind<long, long>(0, (x, y) => x + y);

		foreach (var (key, sum) in gs)
		{
			uf.Add(key, sum);
		}

		for (int i = 1; i < gs.Length; i++)
		{
			if (gs[i].key - gs[i - 1].key == 1)
				uf.Union(gs[i - 1].key, gs[i].key);
		}

		if (gs[0].key == 0 && gs[^1].key == m - 1)
			uf.Union(0, m - 1);

		return gs.Sum(g => g.sum) - gs.Max(g => uf.GetValue(g.key));
	}
}
