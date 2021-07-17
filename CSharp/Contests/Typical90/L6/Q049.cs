using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class Q049
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		es = Array.ConvertAll(es, e => new[] { e[1] - 1, e[2], e[0] });
		var mes = SpanningTreeHelper.Kruskal(n + 1, es);

		if (mes.Length < n) return -1;
		return mes.Sum(e => (long)e[2]);
	}
}

public static class SpanningTreeHelper
{
	// n: 頂点の個数
	// 結果は undirected
	public static int[][] Kruskal(int n, int[][] ues)
	{
		var uf = new UF(n);
		var mes = new List<int[]>();

		foreach (var e in ues.OrderBy(e => e[2]))
		{
			if (uf.AreUnited(e[0], e[1])) continue;
			uf.Unite(e[0], e[1]);
			mes.Add(e);
			// 実際の頂点数に注意。
			// あまり実行速度に影響しないようです。
			//if (mes.Count == n - 1) break;
		}
		return mes.ToArray();
	}

	// n: 頂点の個数
	// 結果は directed
	public static int[][] Prim(int n, int root, int[][] ues) => Prim(n, root, ToMap(n, ues, false));
	public static int[][] Prim(int n, int root, List<int[]>[] map)
	{
		var u = new bool[n];
		var q = PQ<int[]>.Create(e => e[2]);
		u[root] = true;
		q.PushRange(map[root].ToArray());
		var mes = new List<int[]>();

		// 実際の頂点数に注意。
		while (q.Count > 0 && mes.Count < n - 1)
		{
			var e = q.Pop();
			if (u[e[1]]) continue;
			u[e[1]] = true;
			mes.Add(e);
			foreach (var ne in map[e[1]])
				if (ne[1] != e[0])
					q.Push(ne);
		}
		return mes.ToArray();
	}

	static List<int[]>[] ToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
