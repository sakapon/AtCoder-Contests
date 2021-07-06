using System;
using System.Collections.Generic;

class Q071B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var r = TopologicalSort(n + 1, es);
		if (r == null)
		{
			Console.WriteLine(-1);
		}
		else
		{
			Console.WriteLine(string.Join(" ", r[1..]));
		}
	}

	static int[] TopologicalSort(int n, int[][] des)
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
