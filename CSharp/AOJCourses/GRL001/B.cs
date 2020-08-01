using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var u = BellmanFord(h[0] - 1, h[2], -1, es);
		if (u == null) { Console.WriteLine("NEGATIVE CYCLE"); return; }
		Console.WriteLine(string.Join("\n", u.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
	}

	static long[] BellmanFord(int n, int sv, int ev, int[][] dag)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in dag)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
		}

		var from = Enumerable.Repeat(-1, n + 1).ToArray();
		var u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		u[sv] = 0;

		var next = true;
		for (int k = 0; k <= n && next; k++)
		{
			next = false;
			for (int v = 0; v <= n; v++)
			{
				if (u[v] == long.MaxValue) continue;
				foreach (var e in map[v])
				{
					if (u[e[0]] <= u[v] + e[1]) continue;
					from[e[0]] = v;
					u[e[0]] = u[v] + e[1];
					next = true;
				}
			}
		}
		if (next) return null;
		return u;
	}
}
