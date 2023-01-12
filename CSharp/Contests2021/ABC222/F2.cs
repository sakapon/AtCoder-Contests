using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Int.Trees.WeightedTree101;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());
		var d = Read();

		var g = new ListWeightedGraph(2 * n + 1, es, true);
		for (int v = 1; v <= n; v++)
		{
			g.AddEdge(v, n + v, true, d[v - 1]);
		}

		var rn = Enumerable.Range(1, n).ToArray();
		var rn2 = Enumerable.Range(1, n * 2).ToArray();

		var c1 = g.ShortestByBFS(n + 1);
		var root = rn2.FirstMax(v => c1[v]);
		c1 = g.ShortestByBFS(root);
		var end = rn2.FirstMax(v => c1[v]);
		var c2 = g.ShortestByBFS(end);

		var r = rn.Select(v =>
		{
			if (n + v == root)
			{
				return c2[v];
			}
			else if (n + v == end)
			{
				return c1[v];
			}
			else
			{
				return Math.Max(c1[v], c2[v]);
			}
		});

		return string.Join("\n", r);
	}
}
