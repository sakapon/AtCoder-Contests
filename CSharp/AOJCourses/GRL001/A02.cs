using System;
using System.Collections.Generic;
using System.Linq;

class A02
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var d = Dijkstra(h[0] - 1, h[2], -1, es);
		Console.WriteLine(string.Join("\n", d.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static long[] Dijkstra(int n, int sv, int ev, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
		}

		var d = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var u = new bool[n + 1];
		var pq = PQ<VD>.Create(vd => vd.d);
		d[sv] = 0;
		pq.Push(new VD { v = sv, d = 0 });

		while (pq.Count > 0)
		{
			var vd = pq.Pop();
			var v = vd.v;
			if (u[v]) continue;
			u[v] = true;
			foreach (var e in map[v])
			{
				if (d[e[0]] <= d[v] + e[1]) continue;
				d[e[0]] = d[v] + e[1];
				pq.Push(new VD { v = e[0], d = d[e[0]] });
			}
		}
		return d;
	}

	struct VD
	{
		public int v;
		public long d;
	}
}
