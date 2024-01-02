using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		// 0-based
		var graph = new ListUnweightedGraph(n);
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				if (s[i][j] == 'Y') graph.AddEdge(i, j, false);

		var r = new (long d, long val)[qc];
		var comp = Comparer<(long, long)>.Default;

		foreach (var g in qs.Select((q, qi) => (qi, q.u, q.v)).GroupBy(q => q.u, q => (q.qi, q.v)))
		{
			var costs = ShortestByBFS(g.Key - 1);
			foreach (var (qi, ev) in g)
			{
				r[qi] = costs[ev - 1];
			}

			(long, long)[] ShortestByBFS(int sv, int ev = -1)
			{
				var costs = Array.ConvertAll(new bool[graph.VertexesCount], _ => (long.MaxValue, 0L));
				costs[sv] = (0, -a[sv]);
				var q = new Queue<int>();
				q.Enqueue(sv);

				while (q.Count > 0)
				{
					var v = q.Dequeue();
					if (v == ev) return costs;
					var (d, val) = costs[v];

					foreach (var nv in graph.GetEdges(v))
					{
						var nc = (d + 1, val - a[nv]);
						if (comp.Compare(costs[nv], nc) <= 0) continue;
						costs[nv] = nc;
						q.Enqueue(nv);
					}
				}
				return costs;
			}
		}

		return string.Join("\n", r.Select(p => p.d == long.MaxValue ? "Impossible" : $"{p.d} {-p.val}"));
	}
}
