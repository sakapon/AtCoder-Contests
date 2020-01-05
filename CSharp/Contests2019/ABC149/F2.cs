using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()))
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}

		tour = new int[n + 1].Select(_ => new List<int>()).ToArray();
		Dfs(1, 0);

		MInt bSum = 0, m2 = 2, all = m2.Pow(n - 1);
		foreach (var l in tour.Skip(1))
		{
			if (l.Count < 2)
				bSum += all - 1;
			else
				foreach (var c in Subtrees(l, n - 1))
					bSum += m2.Pow(c) - 1;
		}
		Console.WriteLine(((n - (n + bSum) / all) / 2).V);
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

	static IEnumerable<int> Subtrees(List<int> l, int m)
	{
		for (int i = 0; i < l.Count; i++)
			yield return ((l[(i + 1) % l.Count] - l[i]) / 2 + m) % m;
	}
}
