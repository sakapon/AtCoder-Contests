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

		var g = new ListUnweightedGraph(h * w);
		for (int i = 0; i < h; ++i)
			for (int j = 1; j < w; ++j)
			{
				if (s2[i][j] == -1) continue;
				if (s2[i][j - 1] == -1) continue;

				var v = w * i + j;
				var d = s2[i][j] - s2[i][j - 1];
				d = (d + 5) % 5;

				if (d == 1) g.AddEdge(v - 1, v, false);
				if (d == 4) g.AddEdge(v, v - 1, false);
			}
		for (int j = 0; j < w; ++j)
			for (int i = 1; i < h; ++i)
			{
				if (s2[i][j] == -1) continue;
				if (s2[i - 1][j] == -1) continue;

				var v = w * i + j;
				var d = s2[i][j] - s2[i - 1][j];
				d = (d + 5) % 5;

				if (d == 1) g.AddEdge(v - w, v, false);
				if (d == 4) g.AddEdge(v, v - w, false);
			}

		var ev = h * w - 1;
		var r = g.ShortestByBFS(0, ev);
		return r[ev] != long.MaxValue;
	}
}
