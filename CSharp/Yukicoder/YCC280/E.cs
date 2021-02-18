using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		for (int i = 1; i < n; i++)
			if (a[i - 1] == a[i]) return "No";
		for (int i = 2; i < n; i++)
			if (a[i - 2] == a[i]) return "No";

		var es = new List<int[]>();

		for (int i = 1; i < n; i += 2)
			es.Add(new[] { a[i - 1], a[i] });
		for (int i = 2; i < n; i += 2)
			es.Add(new[] { a[i], a[i - 1] });

		var ts = TopologicalSort(m + 1, es.ToArray());
		if (ts == null) return "No";

		var x = new int[m + 1];
		for (int i = 0; i <= m; i++)
			x[ts[i]] = i;

		return "Yes\n" + string.Join(" ", x.Skip(1));
	}

	static int[] TopologicalSort(int n, int[][] des)
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
		var svs = Enumerable.Range(0, n).Where(v => indeg[v] == 0).ToArray();

		foreach (var sv in svs)
		{
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
