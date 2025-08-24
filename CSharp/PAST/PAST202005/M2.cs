using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Graphs;

class M2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());
		var s = int.Parse(Console.ReadLine());
		var k = int.Parse(Console.ReadLine());
		var t = Read();

		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var (u, v) in es)
		{
			map[u].Add(v);
			map[v].Add(u);
		}

		var ts = t.Append(s).ToArray();
		var d = ts
			.Select(sv =>
			{
				var cs = ShortestByBFS(n + 1, v => map[v].ToArray(), sv);
				return Array.ConvertAll(ts, ev => cs[ev]);
			})
			.ToArray();

		var dp = TSP.Execute(k + 1, k, d);
		return dp[(1 << k) - 1].Min();
	}

	public static long[] ShortestByBFS(int n, Func<int, int[]> nexts, int sv, int ev = -1, long maxCost = long.MaxValue)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			if (v == ev) return costs;
			var nc = costs[v] + 1;
			if (nc > maxCost) return costs;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
