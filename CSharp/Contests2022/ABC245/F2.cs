using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var indeg = new int[n + 1];
		foreach (var e in es)
		{
			map[e[1]].Add(e[0]);
			++indeg[e[0]];
		}

		var q = new Queue<int>(Enumerable.Range(1, n).Where(v => indeg[v] == 0));

		while (q.TryDequeue(out var v))
		{
			foreach (var nv in map[v])
			{
				if (--indeg[nv] == 0) q.Enqueue(nv);
			}
		}
		return indeg.Count(v => v > 0);
	}
}
