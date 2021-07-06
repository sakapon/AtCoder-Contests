using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoderLib6.Trees;

class Q071
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var t = Task.Run(Solve);
		if (!t.Wait(2850)) Console.WriteLine(-1);
	}
	static void Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int[]>());
		var indeg = new int[n + 1];
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			++indeg[e[1]];
		}

		var qc = 1 << k;
		var set = new HashSet<string>();

		while (qc-- > 0)
		{
			var r = TopologicalSort(n + 1, map, indeg);
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
	static int[] TopologicalSort(int n, List<int[]>[] map, int[] indeg0)
	{
		var indeg = (int[])indeg0.Clone();

		var r = new List<int>();
		var q = PQ<int>.Create(v => random.Next());
		for (int v = 1; v < n; ++v)
		{
			if (indeg[v] == 0)
			{
				q.Push(v);
			}
		}

		while (q.Count > 0)
		{
			var v = q.Pop();
			r.Add(v);
			foreach (var e in map[v])
			{
				if (--indeg[e[1]] > 0) continue;
				q.Push(e[1]);
			}
		}
		if (r.Count < n - 1) return null;
		return r.ToArray();
	}
}
