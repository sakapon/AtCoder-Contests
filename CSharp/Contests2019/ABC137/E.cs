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

		var d = new Dictionary<int, int> { [1] = 0 };
		var pq = new HashSet<int> { 1 };

		while (pq.Any())
		{
			var p = pq.First();
			pq.Remove(p);

			if (!map.ContainsKey(p)) continue;
			foreach (var r in map[p])
			{
				if (d.ContainsKey(r.B))
				{
					// TODO
				}
				else
				{
					d[r.B] = d[p] + r.C;
					pq.Add(r.B);
				}
			}
		}
		Console.WriteLine(d[h[0]]);
	}
}

struct R
{
	public int A, B, C;
}
