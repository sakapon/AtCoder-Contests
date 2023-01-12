using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Arrays.PathCore111;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read3());
		var s = new int[0];
		if (k > 0) s = Read();

		var map = Array.ConvertAll(new bool[2 * n + 1], _ => new List<int[]>());
		foreach (var (u, v, a) in es)
		{
			map[u + a * n].Add(new[] { u + a * n, v + a * n, 1 });
			map[v + a * n].Add(new[] { v + a * n, u + a * n, 1 });
		}
		foreach (var v in s)
		{
			map[v].Add(new[] { v, v + n, 0 });
			map[v + n].Add(new[] { v + n, v, 0 });
		}

		var costs = map.ToArrays().ShortestByModBFS(2, 1 + n);

		var r = long.MaxValue;
		r = Math.Min(r, costs[n]);
		r = Math.Min(r, costs[n + n]);
		if (r == long.MaxValue) return -1;
		return r;
	}
}
