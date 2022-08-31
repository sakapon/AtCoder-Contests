using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[0] - 1], _ => Read());
		var qs = Array.ConvertAll(new bool[h[1]], _ => Read());

		map = Array.ConvertAll(new bool[h[0] + 1], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		xs = new int[h[0] + 1];
		foreach (var q in qs)
		{
			xs[q[0]] += q[1];
		}

		c = new int[h[0] + 1];
		Dfs(1, 0);
		Console.WriteLine(string.Join(" ", c.Skip(1)));
	}

	static List<int>[] map;
	static int[] xs;
	static int[] c;

	static void Dfs(int v, int pv)
	{
		c[v] = c[pv] + xs[v];
		foreach (var q in map[v]) if (q != pv) Dfs(q, v);
	}
}
