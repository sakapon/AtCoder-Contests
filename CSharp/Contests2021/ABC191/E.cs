using System;
using System.Collections.Generic;
using Bang.Graphs.Int.Spp;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var map = GraphConsole.ReadWeightedMap(n + 1, m, true);

		var r = new long[n + 1];

		for (int ev = 1; ev <= n; ev++)
		{
			var result = ShortestPathCore.Dijkstra(n + 1, v => v == 0 ? map[ev] : map[v], 0, ev);
			r[ev] = result.GetCost(ev);
		}
		Console.WriteLine(string.Join("\n", r[1..]));
	}
}
