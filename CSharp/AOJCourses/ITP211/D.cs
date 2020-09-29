using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var r = new List<Tuple<int, string>>();
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], k = h[1];
		Combination(Enumerable.Range(0, n).ToArray(), k, p => r.Add(Tuple.Create(p.Aggregate(0, (t, i) => t | (1 << i)), string.Join(" ", p))));
		Console.WriteLine(string.Join("\n", r.OrderBy(t => t.Item1).Select(t => $"{t.Item1}: {t.Item2}")));
	}

	static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];

		Action<int, int> Dfs = null;
		Dfs = (i, j0) =>
		{
			for (int j = j0; j < values.Length; ++j)
			{
				p[i] = values[j];
				if (i + 1 < r) Dfs(i + 1, j + 1); else action(p);
			}
		};
		if (r > 0) Dfs(0, 0); else action(p);
	}
}
