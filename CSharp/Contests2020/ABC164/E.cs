using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Grid.Spp;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (n, m, s) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read4());
		var cds = Array.ConvertAll(new bool[n], _ => Read2());

		var jMax = 2500;
		var map = new WeightedMap(n + 1, jMax + 1);

		foreach (var (u, v, a, b) in es)
		{
			for (int j = 0; j + a <= jMax; j++)
			{
				map.AddEdge(new Point(u, j + a), new Point(v, j), b, true);
				map.AddEdge(new Point(v, j + a), new Point(u, j), b, true);
			}
		}
		for (int i = 1; i <= n; i++)
		{
			var (c, d) = cds[i - 1];
			for (int j = 0; j < jMax; j++)
			{
				map.AddEdge(new Point(i, j), new Point(i, Math.Min(jMax, j + c)), d, true);
			}
		}

		s = Math.Min(s, jMax);
		var r = map.Dijkstra((1, s), (-1, -1));
		var rj = Enumerable.Range(0, jMax + 1).ToArray();

		for (int i = 2; i <= n; i++)
		{
			Console.WriteLine(rj.Min(j => r[i, j]));
		}
	}
}
