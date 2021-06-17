using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, n) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read4());

		var sv = 0;
		var ev = 401;

		var es = new List<long[]>();

		for (int i = 1; i <= h; i++)
			es.Add(new[] { sv, i, 1L });
		for (int i = 1; i <= w; i++)
			es.Add(new[] { 300 + i, ev, 1L });

		for (int i = 1; i <= n; i++)
		{
			var (a, b, c, d) = ps[i - 1];
			var ri = 100 + i;
			var ci = 200 + i;
			es.Add(new[] { ri, ci, 1L });

			for (int j = a; j <= c; j++)
				es.Add(new[] { j, ri, 1L });
			for (int j = b; j <= d; j++)
				es.Add(new[] { ci, 300 + j, 1L });
		}

		return MaxFlow(ev, sv, ev, es.ToArray());
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
