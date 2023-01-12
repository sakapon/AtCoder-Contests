using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Arrays.PathCore111;

class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read3());
		var s = Read().ToHashSet();

		var map = Array.ConvertAll(new bool[2 * n + 1], _ => new List<int>());
		foreach (var (u, v, a) in es)
		{
			// 距離 0 で連結している頂点を同値類とします。
			var u2 = a == 1 || s.Contains(u) ? u + n : u;
			var v2 = a == 1 || s.Contains(v) ? v + n : v;
			map[u2].Add(v2);
			map[v2].Add(u2);
		}

		var costs = map.ToArrays().ShortestByBFS(1 + n);

		var r = Math.Min(costs[n], costs[n + n]);
		if (r == long.MaxValue) return -1;
		return r;
	}
}
