using System;
using System.Collections.Generic;
using System.Linq;

class E0
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = new int[n].Select(_ => Read()).ToArray();

		var sv = 2 * n;
		var ev = sv + 1;

		var dg = new List<long[]>();
		for (int i = 0; i < n; i++)
		{
			dg.Add(new[] { sv, i, k, 0L });
			dg.Add(new[] { n + i, ev, k, 0L });
		}

		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
			{
				dg.Add(new[] { i, n + j, 1L, -a[i][j] });
			}

		var M = 0L;
		char[][] s = null;
		MinCostFlow(ev, sv, ev, dg.ToArray(), n * k, (cost, map) =>
		{
			if (-cost <= M) return;

			M = -cost;
			s = NewArray2(n, n, '.');
			for (int i = 0; i < n; i++)
				foreach (var e in map[i])
					if (e[2] == 0 && e[1] != sv)
						s[i][e[1] - n] = 'X';
		});

		Console.WriteLine(M);
		foreach (var r in s) Console.WriteLine(new string(r));
	}

	// dg: { from, to, capacity, cost }
	static long MinCostFlow(int n, int sv, int ev, long[][] dg, long f, Action<long, List<long[]>[]> action)
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
			action(r, map);
		}
		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => NewArrayF(n1, () => NewArray1(n2, v));
	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}

	static T[] NewArrayF<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}
}
