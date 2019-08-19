using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var rs = Enumerable.Range(0, h[1]).Select(i => read()).Select(r => new R { A = r[0], B = r[1], C = r[2] - h[2] }).ToArray();
		map = rs.GroupBy(r => r.A).ToDictionary(g => g.Key, g => g.ToArray());

		var cs = Enumerable.Repeat(long.MinValue, h[0] + 1).ToArray();
		cs[1] = 0;
		long c;
		for (var i = 0; i < h[0]; i++)
			foreach (var r in rs)
				if (cs[r.A] != long.MinValue && cs[r.B] < (c = cs[r.A] + r.C)) cs[r.B] = c;

		foreach (var r in rs)
		{
			if (outs.Contains(r.B)) continue;
			if (cs[r.A] == long.MinValue || cs[r.B] >= cs[r.A] + r.C) continue;

			FindPath(r.B);
			if (outs.Contains(h[0])) { Console.WriteLine(-1); return; }
		}
		Console.WriteLine(Math.Max(cs[h[0]], 0));
	}

	static Dictionary<int, R[]> map;
	static HashSet<int> outs = new HashSet<int>();

	static void FindPath(int p)
	{
		if (!outs.Add(p)) return;
		if (!map.ContainsKey(p)) return;
		foreach (var r in map[p]) FindPath(r.B);
	}
}

struct R
{
	public int A, B, C;
}
