using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var a = new int[h].Select(_ => Console.ReadLine()).ToArray();

		int sv = h + w, ev = sv + 1;
		var s = (i: 0, j: 0);
		var t = (i: 0, j: 0);

		var dag = new List<long[]>();
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (a[i][j] == 'o')
				{
					dag.Add(new[] { i, h + j, 1L });
					dag.Add(new[] { h + j, i, 1L });
				}
				else if (a[i][j] == 'S')
				{
					s = (i, j);
					dag.Add(new[] { sv, i, 1L << 60 });
					dag.Add(new[] { sv, h + j, 1L << 60 });
				}
				else if (a[i][j] == 'T')
				{
					t = (i, j);
					dag.Add(new[] { i, ev, 1L << 60 });
					dag.Add(new[] { h + j, ev, 1L << 60 });
				}

		Console.WriteLine(s.i == t.i || s.j == t.j ? -1 : MaxFlow(ev, sv, ev, dag.ToArray()));
	}

	static long MaxFlow(int n, int sv, int ev, long[][] dag)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var e in dag)
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
