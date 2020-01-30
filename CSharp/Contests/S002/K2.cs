using System;
using System.Collections.Generic;
using System.Linq;

class K2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var d = a.SelectMany(x => x).Distinct().OrderBy(x => x).Select((x, i) => new { x, i }).ToDictionary(_ => _.x, _ => _.i);
		map = a.Concat(a.Select(x => new[] { x[1], x[0] })).GroupBy(x => d[x[0]], x => d[x[1]]).ToDictionary(g => g.Key, g => g.ToArray());
		u = new bool[d.Count];

		var r = 0;
		for (int i = 0; i < d.Count; i++)
		{
			if (u[i]) continue;
			c = 0;
			closed = false;
			Dfs(i, -1);
			r += closed ? c : c - 1;
		}
		Console.WriteLine(r);
	}

	static Dictionary<int, int[]> map;
	static bool[] u;
	static int c;
	static bool closed;
	static void Dfs(int p, int p0)
	{
		u[p] = true;
		++c;
		if (!map.ContainsKey(p)) return;
		foreach (var x in map[p])
		{
			if (x == p0) continue;
			if (u[x]) { closed = true; continue; }
			Dfs(x, p);
		}
	}
}
