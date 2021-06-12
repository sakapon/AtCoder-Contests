using System;
using System.Collections.Generic;
using System.Linq;

class Q040
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, w) = Read2();
		var a = Read();
		var c = Array.ConvertAll(new bool[n], _ => Read()[1..]);

		long amax = a.Max();
		var sv = 0;
		var ev = n + 1;
		var rn = Enumerable.Range(0, n).ToArray();

		var es = new List<long[]>();
		es.AddRange(rn.Select(i => new[] { sv, i + 1, amax + w - a[i] }));
		es.AddRange(rn.Select(i => new[] { i + 1, ev, amax }));

		for (int i = 0; i < n; i++)
			foreach (var nv in c[i])
				es.Add(new long[] { i + 1, nv, 1L << 50 });

		var r = MaxFlow(ev, sv, ev, es.ToArray());
		return n * amax - r;
	}

	// dg: { from, to, capacity }
	static long MaxFlow(int n, int sv, int ev, long[][] dg)
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
		return M;
	}
}
