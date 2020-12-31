using System;
using System.Collections.Generic;
using System.Linq;

class K2
{
	static void Main()
	{
		var (h, w) = (4, 4);
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		Func<(int i, int j), int> toHash = p => p.i * w + p.j;
		var sx = 0;
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (s[i][j] == '#')
					sx |= 1 << toHash((i, j));

		var map = Enumerable.Range(0, h * w).Select(id => new List<int> { id }).ToArray();
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
		Console.WriteLine(GetEV(sx));

		double GetEV(int x)
		{
			if (dp[x] != double.MaxValue) return dp[x];

			for (int v = 0; v < h * w; v++)
			{
				var c = map[v].Count(nv => (x & (1 << nv)) != 0);
				if (c == 0) continue;

				var e = (map[v].Where(nv => (x & (1 << nv)) != 0).Sum(nv => GetEV(x - (1 << nv))) + 5) / c;
				dp[x] = Math.Min(dp[x], e);
			}
			return dp[x];
		}
	}
}
