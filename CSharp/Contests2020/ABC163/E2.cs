using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var dag = new List<long[]>();
		dag.AddRange(Enumerable.Range(1, n).Select(i => new[] { 0, i, 1, 0L }));
		dag.AddRange(Enumerable.Range(n + 1, n).Select(i => new[] { i, 2 * n + 1, 1, 0L }));
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				dag.Add(new[] { i + 1, j + 1 + n, 1, -a[i] * Math.Abs(i - j) });

		Console.WriteLine(-MinCostFlow(2 * n + 1, 0, 2 * n + 1, dag.ToArray(), n));
	}

	static long MinCostFlow(int n, int sv, int ev, long[][] dg, long f)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dg)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2], e[3], map[e[1]].Count });
			map[e[1]].Add(new[] { e[1], e[0], 0, -e[3], map[e[0]].Count - 1 });
		}

		long BellmanFord()
		{
			var from = new long[n + 1][];
			var cost = new long[n + 1];
			Array.Fill(cost, long.MaxValue);
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

		long r = 0, t;
		while (f > 0)
		{
			if ((t = BellmanFord()) == long.MaxValue) return t;
			r += t;
		}
		return r;
	}
}
