﻿using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = new int[n - 1].Select(_ => Read()).ToArray();

		Console.WriteLine(string.Join("\n", Heights(n - 1, 0, es)));
	}

	static long[] Heights(int n, int sv, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
			map[e[1]].Add(new[] { e[0], e[2] });
		}

		var d = Distances(n, sv, map);
		var ev1 = Enumerable.Range(0, n + 1).OrderBy(v => -d[v]).First();
		var d1 = Distances(n, ev1, map);
		var ev2 = Enumerable.Range(0, n + 1).OrderBy(v => -d1[v]).First();
		var d2 = Distances(n, ev2, map);
		return d1.Zip(d2, Math.Max).ToArray();
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
