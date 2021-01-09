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

		var cs = Dijkstra(n, map, 0);
		Console.WriteLine(string.Join("\n", cs.Select((c, v) => $"{v} {c}")));
	}

	static long[] Dijkstra(int n, int[][][] map, int sv, int ev = -1)
	{
		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PQ<int>.CreateWithKey(v => cs[v]);
		cs[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var vc = q.Pop();
			var v = vc.Value;
			if (v == ev) break;
			if (cs[v] < vc.Key) continue;

			foreach (var e in map[v])
			{
				if (cs[e[0]] <= cs[v] + e[1]) continue;
				cs[e[0]] = cs[v] + e[1];
				q.Push(e[0]);
			}
		}
		return cs;
	}
}
