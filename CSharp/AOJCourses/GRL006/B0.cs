using System;
using System.Collections.Generic;
using System.Linq;

class B0
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = (int)h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var r = MinCostFlow(n - 1, 0, n - 1, es, h[2]);
		Console.WriteLine(r == long.MaxValue ? -1 : r);
	}

	static long MinCostFlow(int n, int sv, int ev, long[][] dg, long f)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dg)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2], e[3], map[e[1]].Count });
			map[e[1]].Add(new[] { e[1], e[0], 0, -e[3], map[e[0]].Count - 1 });
		}

		long r = 0, t;
		while (f > 0)
		{
			if ((t = BellmanFord(n, sv, ev, map, ref f)) == long.MaxValue) return t;
			r += t;
		}
		return r;
	}

	static long BellmanFord(int n, int sv, int ev, List<long[]>[] map, ref long f)
	{
		var from = new long[n + 1][];
		var cost = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var minFlow = new long[n + 1];
		cost[sv] = 0;
		minFlow[sv] = f;

		var next = true;
		while (next)
		{
			next = false;
			for (int v = 0; v <= n; v++)
			{
				if (cost[v] == long.MaxValue) continue;
				foreach (var e in map[v])
				{
					if (e[2] == 0 || cost[e[1]] <= cost[v] + e[3]) continue;
					from[e[1]] = e;
					cost[e[1]] = cost[v] + e[3];
					minFlow[e[1]] = Math.Min(minFlow[v], e[2]);
					next = true;
				}
			}
		}

		if (from[ev] == null) return long.MaxValue;
		for (long v = ev; v != sv; v = from[v][0])
		{
			var e = from[v];
			e[2] -= minFlow[ev];
			map[e[1]][(int)e[4]][2] += minFlow[ev];
		}
		f -= minFlow[ev];
		return minFlow[ev] * cost[ev];
	}
}
