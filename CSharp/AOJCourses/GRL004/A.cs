using System;
using System.Collections.Generic;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		var r = DirectedGraphHelper.TopologicalSort(h[0], es);
		Console.WriteLine(r == null ? 1 : 0);
	}
}

public static class DirectedGraphHelper
{
	// 連結性および重みの有無を問いません。
	// 閉路があるとき、null。
	// DAG であるとき、ソートされた頂点集合。
	public static int[] TopologicalSort(int n, int[][] des)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		var indeg = new int[n];
		foreach (var e in des)
		{
			map[e[0]].Add(e);
			++indeg[e[1]];
		}

		var r = new List<int>();
		var q = new Queue<int>();
		var svs = Array.ConvertAll(indeg, x => x == 0);

		// 連結されたグループごとに探索します。
		for (int sv = 0; sv < n; ++sv)
		{
			if (!svs[sv]) continue;

			r.Add(sv);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					r.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}
		}

		if (r.Count < n) return null;
		return r.ToArray();
	}
}
