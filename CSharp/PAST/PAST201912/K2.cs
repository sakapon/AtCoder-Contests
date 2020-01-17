using System;
using System.Collections.Generic;
using System.Linq;

class K2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		map = Enumerable.Range(1, n).GroupBy(i => int.Parse(Console.ReadLine())).ToDictionary(g => g.Key, g => g.ToArray());

		tour = new int[n + 1].Select(_ => new List<int>()).ToArray();
		Dfs(map[-1][0], 0);

		var qs = new int[int.Parse(Console.ReadLine())]
			.Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(q => tour[q[1]][0] <= tour[q[0]][0] && tour[q[0]].Last() <= tour[q[1]].Last() ? "Yes" : "No");
		Console.WriteLine(string.Join("\n", qs));
	}

	static Dictionary<int, int[]> map;
	static List<int>[] tour;
	static int c;
	static void Dfs(int p, int p0)
	{
		tour[p].Add(++c);
		if (!map.ContainsKey(p)) return;
		foreach (var x in map[p])
		{
			if (x == p0) continue;
			Dfs(x, p);
			tour[p].Add(++c);
		}
	}
}
