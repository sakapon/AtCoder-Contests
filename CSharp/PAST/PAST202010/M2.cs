using System;
using System.Collections.Generic;
using System.Linq;

class M2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], qc = h[1];
		var es = new bool[n - 1].Select(_ => Read()).Select((e, ei) => new[] { e[0], e[1], ei + 1 }).ToArray();
		var map = EdgesToMap(n + 1, es, false);

		var colorsMap = Array.ConvertAll(new bool[n + 1], _ => new SortedDictionary<int, int>());
		for (int qi = 0; qi < qc; qi++)
		{
			var q = Read();
			colorsMap[q[0]][-qi] = q[2];
			colorsMap[q[1]][-qi] = q[2];
		}

		var uf = new UF<SortedDictionary<int, int>>(n + 1, (d1, d2) =>
		{
			if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
			foreach (var (qi, c) in d2)
				if (!d1.Remove(qi))
					d1[qi] = c;
			return d1;
		},
		colorsMap);

		var colors = new int[n];

		void Dfs(int v, int pv = -1)
		{
			foreach (var e in map[v])
			{
				if (e[1] == pv) continue;

				Dfs(e[1], v);

				var d = uf.GetValue(e[1]);
				if (d.Any()) colors[e[2]] = d.First().Value;
				uf.Unite(e[1], v);
			}
		}

		Dfs(1);
		Console.WriteLine(string.Join("\n", colors.Skip(1)));
	}

	static List<int[]>[] EdgesToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
