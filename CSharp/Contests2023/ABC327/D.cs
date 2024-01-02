using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read();

		var g = new ListUnweightedGraph(n + 1);
		for (int i = 0; i < m; i++)
		{
			g.AddEdge(a[i], b[i], true);
		}

		var d = Array.ConvertAll(new bool[n + 1], _ => -1);
		var q = new Queue<int>();

		for (int sv = 1; sv <= n; sv++)
		{
			if (d[sv] != -1) continue;

			d[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nd = 1 - d[v];

				foreach (var nv in g.GetEdges(v))
				{
					if (d[nv] == -1)
					{
						d[nv] = nd;
						q.Enqueue(nv);
					}
					else
					{
						if (d[nv] != nd) return false;
					}
				}
			}
		}
		return true;
	}
}
