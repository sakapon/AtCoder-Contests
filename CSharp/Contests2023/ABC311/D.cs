using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		const char wall = '#';
		var n = h * w;
		var g = new ListUnweightedGraph(5 * n);

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == wall) continue;

				var v = w * i + j;
				for (int k = 1; k < 5; k++)
				{
					g.AddEdge(v, k * n + v, false);
				}
			}

		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				var v = w * i + j;

				if (s[i][j] == wall)
				{
					if (s[i][j - 1] == wall) { }
					else
					{
						g.AddEdge(1 * n + v - 1, v - 1, false);
					}
				}
				else
				{
					if (s[i][j - 1] == wall)
					{
						g.AddEdge(2 * n + v, v, false);
					}
					else
					{
						g.AddEdge(1 * n + v - 1, 1 * n + v, false);
						g.AddEdge(2 * n + v, 2 * n + v - 1, false);
					}
				}
			}

		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				var v = w * i + j;

				if (s[i][j] == wall)
				{
					if (s[i - 1][j] == wall) { }
					else
					{
						g.AddEdge(3 * n + v - w, v - w, false);
					}
				}
				else
				{
					if (s[i - 1][j] == wall)
					{
						g.AddEdge(4 * n + v, v, false);
					}
					else
					{
						g.AddEdge(3 * n + v - w, 3 * n + v, false);
						g.AddEdge(4 * n + v, 4 * n + v - w, false);
					}
				}
			}

		var r = g.ConnectivityByDFS(w + 1);
		var r5 = new[] { 0, 1, 2, 3, 4 };
		return Enumerable.Range(0, n).Count(v => r5.Any(k => r[k * n + v]));
	}
}
