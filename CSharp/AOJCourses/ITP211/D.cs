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
		new Comb<int>().Find(Enumerable.Range(0, n).ToArray(), k, p => r.Add(Tuple.Create(p.Aggregate(0, (t, i) => t | (1 << i)), string.Join(" ", p))));
		Console.WriteLine(string.Join("\n", r.OrderBy(t => t.Item1).Select(t => $"{t.Item1}: {t.Item2}")));
	}
}

class Comb<T>
{
	T[] v, p;
	Action<T[]> act;

	public void Find(T[] _v, int r, Action<T[]> _act)
	{
		v = _v; p = new T[r]; act = _act;
		if (p.Length > 0) Dfs(0, 0); else act(p);
	}

	void Dfs(int i, int j0)
	{
		for (int j = j0; j < v.Length; ++j)
		{
			p[i] = v[j];
			if (i + 1 < p.Length) Dfs(i + 1, j + 1); else act(p);
		}
	}
}
