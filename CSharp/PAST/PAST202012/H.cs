using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Spp;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		Point sv = Read2();
		var s = GraphConsole.ReadGridAsChar(h);
		GridHelper.EncloseGrid(ref h, ref w, ref s, '#');

		var spp = ShortestPath.ForGrid(h, w)
			.ForUnweightedMap(v =>
			{
				var nvs = new List<Point>();
				Point nv;

				{
					var c = s.GetValue(nv = v + new Point(-1, 0));
					if (c == '.' || c == 'v') nvs.Add(nv);
				}
				{
					var c = s.GetValue(nv = v + new Point(1, 0));
					if (c == '.' || c == '^') nvs.Add(nv);
				}
				{
					var c = s.GetValue(nv = v + new Point(0, -1));
					if (c == '.' || c == '>') nvs.Add(nv);
				}
				{
					var c = s.GetValue(nv = v + new Point(0, 1));
					if (c == '.' || c == '<') nvs.Add(nv);
				}

				return nvs.ToArray();
			})
			.Bfs(sv, (-1, -1));

		var s2 = NewArray2<char>(h - 2, w - 2);
		for (int i = 1; i < h - 1; i++)
			for (int j = 1; j < w - 1; j++)
			{
				s2[i - 1][j - 1] = s[i][j] == '#' ? '#' : spp.IsConnected((i, j)) ? 'o' : 'x';
			}

		Console.WriteLine(string.Join("\n", s2.Select(l => string.Join("", l))));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
