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

		var s = (i: 0, j: 0);
		var t = (i: 0, j: 0);

		var dag = new List<long[]>();
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (a[i][j] == 'o')
				{
					dag.Add(new[] { i + 1, h + j + 1, 1L });
					dag.Add(new[] { h + j + 1, i + 1, 1L });
				}
				else if (a[i][j] == 'S')
				{
					s = (i, j);
					dag.Add(new[] { 0, i + 1, 1L << 60 });
					dag.Add(new[] { 0, h + j + 1, 1L << 60 });
				}
				else if (a[i][j] == 'T')
				{
					t = (i, j);
					dag.Add(new[] { i + 1, h + w + 1, 1L << 60 });
					dag.Add(new[] { h + j + 1, h + w + 1, 1L << 60 });
				}

		if (s.i == t.i || s.j == t.j) { Console.WriteLine(-1); return; }
		Console.WriteLine(MaxFlow(h + w + 1, 0, h + w + 1, dag.ToArray()));
	}

	static long MaxFlow(int n, int sv, int ev, long[][] dag)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<long[]>());
		foreach (var r in dag)
		{
			map[r[0]].Add(new[] { r[1], r[2], map[r[1]].Count });
			map[r[1]].Add(new[] { r[0], 0, map[r[0]].Count - 1 });
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
				foreach (var r in map[v])
				{
					if (u[r[0]] || r[1] == 0) continue;
					from[r[0]] = r;
					u[r[0]] = true;
					minFlow[r[0]] = Math.Min(minFlow[v], r[1]);
					q.Enqueue(r[0]);
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
