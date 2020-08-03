using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int nx = (int)h[0], ny = (int)h[1], sv = nx + ny, ev = sv + 1;
		var es = new int[h[2]].Select(_ => Read()).Select(x => new[] { x[0], nx + x[1], 1 }).ToList();

		es.AddRange(Enumerable.Range(0, nx).Select(i => new[] { sv, i, 1L }));
		es.AddRange(Enumerable.Range(0, ny).Select(j => new[] { nx + j, ev, 1L }));
		Console.WriteLine(MaxFlow(ev, sv, ev, es.ToArray()));
	}

	static long MaxFlow(int n, int sv, int ev, long[][] dg)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dg)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2], map[e[1]].Count });
			map[e[1]].Add(new[] { e[1], e[0], 0, map[e[0]].Count - 1 });
		}

		long M = 0, t;
		while ((t = Bfs(n, sv, ev, map)) > 0) M += t;
		return M;
	}

	static long Bfs(int n, int sv, int ev, List<long[]>[] map)
	{
		var from = new long[n + 1][];
		var minFlow = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var q = new Queue<long>();
		q.Enqueue(sv);

		while (q.Any())
		{
			var v = q.Dequeue();
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
}
