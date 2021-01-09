using System;
using System.Collections.Generic;
using System.Linq;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var es = Array.ConvertAll(new bool[m], _ => Read()).Select(e => new[] { e[0] - 1, e[1], e[2] }).ToList();

		for (int i = 0; i < n; i++)
		{
			es.Add(new[] { i, i + 1, (int)a[i] });
			es.Add(new[] { i + 1, i, 0 });
		}
		Console.WriteLine(a.Sum() - Dijkstra(n + 1, es.ToArray(), true, 0, n).Item1[n]);
	}

	// es: { from, to, cost }
	// 最小コスト: 到達不可能の場合、MaxValue。
	// 入辺: 到達不可能の場合、null。
	public static Tuple<long[], int[][]> Dijkstra(int n, int[][] es, bool directed, int sv, int ev = -1)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var inEdges = new int[n][];
		var q = PQ<int>.CreateWithKey(v => cs[v]);
		cs[sv] = 0;
		q.Push(sv);

		while (q.Count > 0)
		{
			var vc = q.Pop();
			var v = vc.Value;
			if (v == ev) break;
			if (cs[v] < vc.Key) continue;

			foreach (var e in map[v])
			{
				if (cs[e[1]] <= cs[v] + e[2]) continue;
				cs[e[1]] = cs[v] + e[2];
				inEdges[e[1]] = e;
				q.Push(e[1]);
			}
		}
		return Tuple.Create(cs, inEdges);
	}
}
