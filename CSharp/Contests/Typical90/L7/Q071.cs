using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class Q071
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map0 = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var indeg = new int[n + 1];
		foreach (var e in es)
		{
			map0[e[0]].Add(e[1]);
			++indeg[e[1]];
		}
		var map = Array.ConvertAll(map0, l => l.ToArray());
		var rn = Enumerable.Range(1, n).ToArray();
		var svs = Array.FindAll(rn, v => indeg[v] == 0);

		var qc = 1 << k;
		var set = new HashSet<string>();

		while (qc-- > 0)
		{
			var r = TopologicalSort(n + 1, map, indeg, svs);
			if (r == null) break;

			set.Add(string.Join(" ", r));
			if (set.Count == k) break;
		}

		if (set.Count < k)
		{
			Console.WriteLine(-1);
		}
		else
		{
			foreach (var s in set)
				Console.WriteLine(s);
		}
	}

	static Random random = new Random();
	static int[] TopologicalSort(int n, int[][] map, int[] indeg0, int[] svs)
	{
		var indeg = (int[])indeg0.Clone();

		var r = new List<int>();
		var q = PQ<int>.Create(v => random.Next());
		q.PushRange(svs);

		while (q.Count > 0)
		{
			var v = q.Pop();
			r.Add(v);
			foreach (var nv in map[v])
			{
				if (--indeg[nv] > 0) continue;
				q.Push(nv);
			}
		}
		if (r.Count < n - 1) return null;
		return r.ToArray();
	}
}
