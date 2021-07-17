using System;
using System.Collections.Generic;
using System.Linq;

class Q062B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		for (int i = 1; i <= n; i++)
		{
			var (a, b) = es[i - 1];
			map[i == a ? 0 : a].Add(i);
			map[i == b ? 0 : b].Add(i);
		}

		var d = Bfs(n + 1, v => map[v].ToArray(), 0);

		if (d.Any(x => x == long.MaxValue)) return -1;
		return string.Join("\n", Enumerable.Range(1, n).OrderBy(v => -d[v]));
	}

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
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
