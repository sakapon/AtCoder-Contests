using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = Read();
		var b = Read();
		var r = Read();

		var dag = new List<long[]>();
		dag.AddRange(Enumerable.Range(1, 3).Select(i => new[] { 0, i, m, 0L }));
		for (int j = 0; j < n; j++)
		{
			long p = a[j] * b[j];
			for (int i = 1; i <= 3; i++, p *= b[j])
				dag.Add(new[] { i, j + 10, 1, -(p % r[i - 1]) });

			dag.Add(new[] { j + 10, 4, 1, p = a[j] * b[j] });
			dag.Add(new[] { j + 10, 4, 1, p *= b[j] - 1 });
			dag.Add(new[] { j + 10, 4, 1, p *= b[j] });
		}

		Console.WriteLine(-MinCostFlow(n + 9, 0, 4, dag.ToArray(), 3 * m));
	}

	static long MinCostFlow(int n, int sv, int ev, long[][] dag, long f)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dag)
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
