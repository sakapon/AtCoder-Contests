using System;
using System.Collections.Generic;
using System.Linq;

class Q077
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read2());
		var b = Array.ConvertAll(new bool[n], _ => Read2());

		var sv = 2 * n;
		var ev = 2 * n + 1;
		var rn = Enumerable.Range(0, n).ToArray();
		var bMap = rn.ToDictionary(i => b[i], i => n + i);
		var nexts = new[] { (t, 0), (t, t), (0, t), (-t, t), (-t, 0), (-t, -t), (0, -t), (t, -t) };

		var es = new List<long[]>();
		es.AddRange(rn.Select(i => new[] { sv, i, 1L }));
		es.AddRange(rn.Select(i => new[] { n + i, ev, 1L }));

		for (int i = 0; i < n; i++)
		{
			var (x, y) = a[i];
			foreach (var (dx, dy) in nexts)
			{
				var np = (x + dx, y + dy);
				if (bMap.ContainsKey(np))
					es.Add(new[] { i, bMap[np], 1L });
			}
		}

		var (M, map) = MaxFlow(ev, sv, ev, es.ToArray());
		if (M < n) return "No";

		var r = Array.ConvertAll(rn, i =>
		{
			var j = 0;
			foreach (var e in map[i])
			{
				if (e[2] == 0)
				{
					j = (int)e[1] - n;
					break;
				}
			}

			var (x, y) = a[i];
			var (nx, ny) = b[j];
			return Array.IndexOf(nexts, (nx - x, ny - y)) + 1;
		});
		return "Yes\n" + string.Join(" ", r);
	}

	// dg: { from, to, capacity }
	static (long, List<long[]>[]) MaxFlow(int n, int sv, int ev, long[][] dg)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dg)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2], map[e[1]].Count });
			map[e[1]].Add(new[] { e[1], e[0], 0, map[e[0]].Count - 1 });
		}

		long Bfs()
		{
			var from = new long[n + 1][];
			var minFlow = new long[n + 1];
			Array.Fill(minFlow, long.MaxValue);
			var q = new Queue<long>();
			q.Enqueue(sv);

			while (q.TryDequeue(out var v))
			{
				if (v == ev) break;
				foreach (var e in map[v])
				{
					if (from[e[1]] != null || e[2] == 0) continue;
					from[e[1]] = e;
					minFlow[e[1]] = Math.Min(minFlow[v], e[2]);
					q.Enqueue(e[1]);
				}
			}

			if (from[ev] == null) return 0;
			for (long v = ev; v != sv; v = from[v][0])
			{
				var e = from[v];
				e[2] -= minFlow[ev];
				map[e[1]][(int)e[3]][2] += minFlow[ev];
			}
			return minFlow[ev];
		}

		long M = 0, t;
		while ((t = Bfs()) > 0) M += t;
		return (M, map);
	}
}
