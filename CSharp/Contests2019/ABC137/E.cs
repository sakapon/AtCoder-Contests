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
		var map = rs.GroupBy(r => r.A).ToDictionary(g => g.Key, g => g.ToArray());

		var d = new Dictionary<int, long> { [1] = 0 };

		for (var i = 1; i < h[0]; i++)
			foreach (var p in d.Keys.ToArray())
			{
				if (!map.ContainsKey(p)) continue;
				foreach (var r in map[p])
				{
					var c = d[p] + r.C;
					if (!d.ContainsKey(r.B) || d[r.B] < c) d[r.B] = c;
				}
			}
		var M = d[h[0]];
		if (d.Keys.Where(p => map.ContainsKey(p)).SelectMany(p => map[p]).All(r => d[r.B] >= d[r.A] + r.C)) { Console.WriteLine(M < 0 ? 0 : M); return; }

		for (var i = 1; i < h[0]; i++)
			foreach (var p in d.Keys.ToArray())
			{
				if (!map.ContainsKey(p)) continue;
				foreach (var r in map[p])
				{
					var c = d[p] + r.C;
					if (!d.ContainsKey(r.B) || d[r.B] < c)
						if (r.B == h[0]) { Console.WriteLine(-1); return; }
						else d[r.B] = c;
				}
			}
		Console.WriteLine(M < 0 ? 0 : M);
	}
}

struct R
{
	public int A, B, C;
}
