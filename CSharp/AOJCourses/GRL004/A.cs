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
	// 閉路検査としても利用できます。O(n + m)
	// 連結性、多重性および重み (e[2]) の有無を問いません。
	// 連結成分の順になるとは限りません。
	// 連結成分ごとに出力するには、先に DSU などで分割してから呼び出します。
	// 閉路があるとき、null。
	// DAG であるとき、ソートされた頂点集合。
	public static int[] TopologicalSort(int n, int[][] des)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		var indeg = new int[n];
		foreach (var e in des)
		{
			map[e[0]].Add(e[1]);
			++indeg[e[1]];
		}

		var r = new List<int>();
		var q = new Queue<int>();
		for (int v = 0; v < n; ++v)
			if (indeg[v] == 0)
				q.Enqueue(v);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			r.Add(v);
			foreach (var nv in map[v])
			{
				if (--indeg[nv] > 0) continue;
				q.Enqueue(nv);
			}
		}

		if (r.Count < n) return null;
		return r.ToArray();
	}
}
