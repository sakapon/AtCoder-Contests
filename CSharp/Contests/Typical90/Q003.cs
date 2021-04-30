using System;
using System.Collections.Generic;
using System.Linq;

class Q003
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		return Diameter(n, 1, es.Select(e => new[] { e[0], e[1], 1 }).ToArray()) + 1;
	}

	static long Diameter(int n, int sv, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
			map[e[1]].Add(new[] { e[0], e[2] });
		}

		var d = Distances(n, sv, map);
		var ev = Enumerable.Range(0, n + 1).OrderBy(v => -d[v]).First();
		return Distances(n, ev, map).Max();
	}

	static long[] Distances(int n, int sv, List<int[]>[] map)
	{
		var from = new int[n + 1];
		var d = new long[n + 1];
		var q = new Queue<int>();
		from[sv] = -1;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var e in map[v])
			{
				if (e[0] == from[v]) continue;
				from[e[0]] = v;
				d[e[0]] = d[v] + e[1];
				q.Enqueue(e[0]);
			}
		}
		return d;
	}
}
