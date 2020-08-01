using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		Console.WriteLine(string.Join("\n", Dijklmna(h[0] - 1, h[2], -1, es).Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static long[] Dijklmna(int n, int sv, int ev, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
		}

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
