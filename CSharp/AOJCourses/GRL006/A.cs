using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = (int)h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		Console.WriteLine(MaxFlow(n - 1, 0, n - 1, es));
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
