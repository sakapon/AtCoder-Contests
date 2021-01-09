using System;
using System.Collections.Generic;
using Bang.Graphs.Spp;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = GraphConsole.ReadEnclosedGrid(ref h, ref w);

		var sv = (h - 2, w - 1);
		var ev = (1, 1);

		var r = ShortestPath.ForGrid(h, w)
			.ForWeightedMap(v =>
			{
				var nes = new List<Edge<Point>>();
				var uv = v - new Point(1, 0);
				var lv = v - new Point(0, 1);
				if (s.GetValue(uv) != '#') nes.Add(new Edge<Point>(v, uv, s.GetValue(uv) == 'S' ? 0 : 1));
				if (s.GetValue(lv) != '#') nes.Add(new Edge<Point>(v, lv, s.GetValue(lv) == 'E' ? 0 : 1));
				return nes.ToArray();
			})
			.BfsMod(2, sv, ev);

		Console.WriteLine(r[ev]);
	}
}
