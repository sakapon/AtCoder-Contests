using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var sv = h + w;
		var ev = sv + 1;

		var es = new List<long[]>();
		es.AddRange(Enumerable.Range(0, h).Select(i => new[] { sv, i, 1L }));
		es.AddRange(Enumerable.Range(0, w).Select(j => new[] { h + j, ev, 1L }));

		var d = new Dictionary<int, List<long[]>>();

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				var k = a[i][j];
				if (k == 0) continue;

				if (!d.ContainsKey(k)) d[k] = new List<long[]>();
				d[k].Add(new[] { i, h + j, 1L });
			}
		}

		var r = 0L;

		foreach (var es2 in d.Values)
		{
			if (es2.Count == 1)
			{
				r++;
				continue;
			}

			es2.AddRange(es);
			r += MaxFlow(ev, sv, ev, es2.ToArray());
		}
		Console.WriteLine(r);
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
