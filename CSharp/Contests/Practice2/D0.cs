using System;
using System.Collections.Generic;
using System.Linq;

class D0
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var s = new int[n].Select(_ => Console.ReadLine().ToCharArray()).ToArray();

		var sv = n * m;
		var ev = sv + 1;

		var vs0 = new List<int>();
		var vs1 = new HashSet<int>();

		// checker board
		var dg = new List<long[]>();
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				if (s[i][j] == '#') continue;

				var v = m * i + j;
				if ((i + j) % 2 == 0)
				{
					dg.Add(new[] { sv, v, 1L });
					vs0.Add(v);
				}
				else
				{
					dg.Add(new[] { v, ev, 1L });
					vs1.Add(v);
				}
			}

		foreach (var v in vs0)
		{
			if (vs1.Contains(v - m)) dg.Add(new[] { v, v - m, 1L });
			if (vs1.Contains(v + m)) dg.Add(new[] { v, v + m, 1L });
			if (v % m != 0 && vs1.Contains(v - 1)) dg.Add(new[] { v, v - 1, 1L });
			if (v % m != m - 1 && vs1.Contains(v + 1)) dg.Add(new[] { v, v + 1, 1L });
		}

		var (M, map) = MaxFlow(ev, sv, ev, dg.ToArray());

		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				if (s[i][j] == '#') continue;

				var v = m * i + j;
				if ((i + j) % 2 == 0)
				{
					var v2 = map[v].FirstOrDefault(e => e[2] == 0 & e[1] != sv)?[1];
					if (v2 == null) continue;

					var (i2, j2) = ((int)v2 / m, (int)v2 % m);
					if (i == i2)
					{
						s[i][Math.Min(j, j2)] = '>';
						s[i][Math.Max(j, j2)] = '<';
					}
					else
					{
						s[Math.Min(i, i2)][j] = 'v';
						s[Math.Max(i, i2)][j] = '^';
					}
				}
			}

		Console.WriteLine(M);
		foreach (var r in s) Console.WriteLine(new string(r));
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
