using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Spp;

class K
{
	static void Main()
	{
		var (h, w) = (4, 4);
		var s = GraphConsole.ReadGrid(h);

		var rhw = Enumerable.Range(0, h * w).ToArray();
		var toHash = GridHelper.CreateToHash(w);
		var fromHash = GridHelper.CreateFromHash(w);

		var sx = rhw
			.Where(i => s.GetValue(fromHash(i)) == '#')
			.Aggregate(0, (p, i) => p | (1 << i));

		var map = rhw.Select(id => new List<int> { id }).ToArray();
		for (int i = 0; i < h; i++)
			for (int j = 1; j < w; j++)
			{
				map[toHash((i, j))].Add(toHash((i, j - 1)));
				map[toHash((i, j - 1))].Add(toHash((i, j)));
			}
		for (int j = 0; j < w; j++)
			for (int i = 1; i < h; i++)
			{
				map[toHash((i, j))].Add(toHash((i - 1, j)));
				map[toHash((i - 1, j))].Add(toHash((i, j)));
			}

		var dp = Array.ConvertAll(new bool[1 << 16], _ => double.MaxValue);
		dp[0] = 0;
		for (int x = 1; x < 1 << 16; x++)
		{
			for (int v = 0; v < h * w; v++)
			{
				var c = map[v].Count(nv => (x & (1 << nv)) != 0);
				if (c == 0) continue;

				var e = (map[v].Where(nv => (x & (1 << nv)) != 0).Sum(nv => dp[x - (1 << nv)]) + 5) / c;
				dp[x] = Math.Min(dp[x], e);
			}
		}
		Console.WriteLine(dp[sx]);
	}
}
