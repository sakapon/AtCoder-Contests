using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		if (s[0][0] != 's') return false;

		var map = new int[1 << 7];
		Array.Fill(map, -1);
		map['s'] = 0;
		map['n'] = 1;
		map['u'] = 2;
		map['k'] = 3;
		map['e'] = 4;

		var s2 = Array.ConvertAll(s, t => t.Select(c => map[c]).ToArray());

		var grid = new UnweightedGrid(h, w);
		var g = new ListUnweightedGraph(h * w);

		for (int v = 0; v < grid.VertexesCount; v++)
		{
			var (i, j) = (v / w, v % w);
			if (s2[i][j] == -1) continue;

			foreach (var nv in grid.GetEdges(v))
			{
				var (ni, nj) = (nv / w, nv % w);
				if (s2[ni][nj] == -1) continue;
				if ((s2[ni][nj] - s2[i][j] + 5) % 5 == 1) g.AddEdge(v, nv, false);
			}
		}

		var ev = h * w - 1;
		var r = g.ShortestByBFS(0, ev);
		return r[ev] != long.MaxValue;
	}
}
