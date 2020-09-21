using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(e => Enumerable.Range(1, e[1]).Select(i => new[] { e[2 * i], e[2 * i + 1] }).ToArray())
			.ToArray();

		var r = Dijkstra(n - 1, 0, -1, map);
		Console.WriteLine(string.Join("\n", r.Select((v, i) => $"{i} {v}")));
	}

	static long[] Dijkstra(int n, int sv, int ev, int[][][] map)
	{
		var d = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var u = new bool[n + 1];
		var pq = PQ<VC>.Create(_ => _.c);
		d[sv] = 0;
		pq.Push(new VC { v = sv, c = d[sv] });

		while (pq.Count > 0)
		{
			var vc = pq.Pop();
			var v = vc.v;
			if (u[v]) continue;
			u[v] = true;

			foreach (var e in map[v])
			{
				if (d[e[0]] <= d[v] + e[1]) continue;
				d[e[0]] = d[v] + e[1];
				pq.Push(new VC { v = e[0], c = d[e[0]] });
			}
		}

		return d;
	}

	struct VC
	{
		public int v;
		public long c;
	}
}
