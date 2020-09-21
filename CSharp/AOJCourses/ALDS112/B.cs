using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var map = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(e => Enumerable.Range(1, e[1]).Select(i => new[] { e[2 * i], e[2 * i + 1] }).ToArray())
			.ToArray();

		var r = Dijklmna(n - 1, 0, -1, map);
		Console.WriteLine(string.Join("\n", r.Select((v, i) => $"{i} {v}")));
	}

	static long[] Dijklmna(int n, int sv, int ev, int[][][] map)
	{
		var u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var q = new Queue<int>();
		u[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var e in map[v])
			{
				if (u[e[0]] <= u[v] + e[1]) continue;
				u[e[0]] = u[v] + e[1];
				q.Enqueue(e[0]);
			}
		}

		return u;
	}
}
