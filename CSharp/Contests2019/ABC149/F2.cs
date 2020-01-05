using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var m = n - 1;
		map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in new int[m].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()))
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}

		tour = new int[n + 1].Select(_ => new List<int>()).ToArray();
		Dfs(1, 0);

		Console.WriteLine((tour.Select(l => Hole(l, m)).Aggregate((x, y) => x + y) / 2).V);
	}

	static List<int>[] map;
	static List<int>[] tour;
	static int c;
	static void Dfs(int p, int p0)
	{
		foreach (var x in map[p])
		{
			if (x == p0) continue;
			tour[x].Add(++c);
			Dfs(x, p);
			tour[p].Add(++c);
		}
	}

	static MInt Hole(List<int> l, int m)
	{
		if (l.Count < 2) return 0;

		MInt allWhite = 1, bSum = 0;
		for (int i = 0; i < l.Count; i++)
		{
			var c = ((l[(i + 1) % l.Count] - l[i]) / 2 + m) % m;
			var w = 1 / ((MInt)2).Pow(c);

			allWhite *= w;
			bSum += 1 / w - 1;
		}
		return 1 - allWhite * (1 + bSum);
	}
}
