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
		foreach (var r in dag)
		{
			map[r[0]].Add(new[] { r[1], r[2], r[3], map[r[1]].Count });
			map[r[1]].Add(new[] { r[0], 0, -r[3], map[r[0]].Count - 1 });
		}

		long[][] from;
		long[] u, minFlow;
		void BellmanFord()
		{
			from = new long[n + 1][];
			u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			minFlow = new long[n + 1];
			u[sv] = 0;
			minFlow[sv] = long.MaxValue;
			long t;
			var next = true;
			while (next)
			{
				next = false;
				for (int v = 0; v <= n; v++)
				{
					if (u[v] == long.MaxValue) continue;
					foreach (var r in map[v])
					{
						if (r[1] == 0 || (t = u[v] + r[2]) >= u[r[0]]) continue;
						from[r[0]] = r;
						u[r[0]] = t;
						minFlow[r[0]] = Math.Min(minFlow[v], r[1]);
						next = true;
					}
				}
			}
		}

		var cost = 0L;
		while (f > 0)
		{
			BellmanFord();
			if (from[ev] == null) break;

			var delta = Math.Min(minFlow[ev], f);
			f -= delta;
			cost += delta * u[ev];

			long v = ev;
			while (true)
			{
				from[v][1] -= delta;
				var rev = map[from[v][0]][(int)from[v][3]];
				rev[1] += delta;
				if ((v = rev[0]) == sv) break;
			}
		}
		return cost;
	}
}
