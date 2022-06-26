using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMapList(n + 1, es);

		// v0: Fake
		var r1 = Bfs(n + 1, v => map[v].ToArray(), 1);
		var rn = Bfs(n + 1, v => map[v].ToArray(), n);

		return string.Join(" ", Enumerable.Range(1, n).Select(v =>
		{
			var r = r1[n];
			if (r1[0] < long.MaxValue && rn[0] < long.MaxValue) r = Math.Min(r, r1[0] + rn[0]);
			if (r1[0] < long.MaxValue && rn[v] < long.MaxValue) r = Math.Min(r, r1[0] + rn[v]);
			if (r1[v] < long.MaxValue && rn[0] < long.MaxValue) r = Math.Min(r, r1[v] + rn[0]);
			return r == long.MaxValue ? -1 : r;
		}));
	}

	public static List<int>[] ToMapList(int n, int[][] es)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			if (e[0] != 0) map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}
		return map;
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
