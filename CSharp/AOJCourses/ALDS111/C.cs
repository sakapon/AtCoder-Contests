using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var map = new int[n + 1][];
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			map[a[0]] = a.Skip(2).ToArray();
		}

		var r = Bfs(n + 1, v => map[v], 1);

		for (int v = 1; v <= n; v++)
			Console.WriteLine($"{v} {(r[v] == long.MaxValue ? -1 : r[v])}");
	}

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.TryDequeue(out var v))
		{
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
