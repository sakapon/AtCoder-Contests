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
			map[e[0]].Add(new[] { e[1], e[2], map[e[1]].Count });
			map[e[1]].Add(new[] { e[0], 0, map[e[0]].Count - 1 });
		}

		long Bfs()
		{
			var from = new long[n + 1][];
			var u = new bool[n + 1];
			var minFlow = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			var q = new Queue<long>();
			q.Enqueue(sv);

			while (q.TryDequeue(out var v))
			{
				if (v == ev) break;
				foreach (var e in map[v])
				{
					if (u[e[0]] || e[1] == 0) continue;
					from[e[0]] = e;
					u[e[0]] = true;
					minFlow[e[0]] = Math.Min(minFlow[v], e[1]);
					q.Enqueue(e[0]);
				}
			}

			if (from[ev] == null) return 0;
			long tv = ev;
			while (true)
			{
				from[tv][1] -= minFlow[ev];
				var rev = map[from[tv][0]][(int)from[tv][2]];
				rev[1] += minFlow[ev];
				if ((tv = rev[0]) == sv) break;
			}
			return minFlow[ev];
		}

		long M = 0, t;
		while ((t = Bfs()) > 0) M += t;
		return M;
	}
}
