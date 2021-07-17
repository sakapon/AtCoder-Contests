using System;
using System.Collections.Generic;
using Bang.Graphs.Spp;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (h, w) = Read2();
		var z = Read();
		var sv = (z[0], z[1]);
		var ev = (z[2], z[3]);
		var s = GraphConsole.ReadEnclosedGrid(ref h, ref w);

		var r1 = ShortestPath.ForGrid(h, w)
			.ForUnweightedMap(v =>
			{
				var nvs = (v.i + v.j) % 2 == 0 ? Nexts1(v) : Nexts2(v);
				return Array.FindAll(nvs, nv => s.GetValue(nv) != '#');
			})
			.Bfs(sv, ev);
		var r2 = ShortestPath.ForGrid(h, w)
			.ForUnweightedMap(v =>
			{
				var nvs = (v.i + v.j) % 2 != 0 ? Nexts1(v) : Nexts2(v);
				return Array.FindAll(nvs, nv => s.GetValue(nv) != '#');
			})
			.Bfs(sv, ev);

		var m = Math.Min(r1[ev], r2[ev]);
		Console.WriteLine(m == long.MaxValue ? -1 : m);

		Point[] Nexts1(Point v) => new[] { new Point(v.i - 1, v.j), new Point(v.i + 1, v.j) };
		Point[] Nexts2(Point v) => new[] { new Point(v.i, v.j - 1), new Point(v.i, v.j + 1) };
	}
}
