using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Int.Others.DirectedGraphEx101;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var rows = a
			.Where(vs => vs.Any(x => x != 0))
			.Select(vs => (min: vs.Where(x => x != 0).Min(), max: vs.Where(x => x != 0).Max()))
			.ToArray();
		Array.Sort(rows);
		if (rows.Length > 0 && !Enumerable.Range(0, rows.Length - 1).All(i => rows[i].max <= rows[i + 1].min)) return false;

		var n = h * w;
		var g = new ListUnweightedGraph(n + w);
		var rw = Enumerable.Range(0, w).ToArray();

		for (int i = 0; i < h; i++)
		{
			var gs = rw
				.Where(j => a[i][j] != 0)
				.GroupBy(j => a[i][j])
				.OrderBy(g => g.Key)
				.Select(g => g.ToArray())
				.ToArray();

			for (int k = 1; k < gs.Length; k++)
			{
				foreach (var j in gs[k - 1])
				{
					g.AddEdge(j + n, w * i + k, false);
				}
				foreach (var j in gs[k])
				{
					g.AddEdge(w * i + k, j + n, false);
				}
			}
		}

		var r = g.TopologicalSort();
		return r != null;
	}
}
