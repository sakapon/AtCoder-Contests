using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.Spp;

class Q013
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var map = GraphConsole.ReadWeightedMap(n + 1, m, false);

		var r1 = map.Dijkstra(1);
		var r2 = map.Dijkstra(n);

		return string.Join("\n", Enumerable.Range(1, n).Select(v => r1[v] + r2[v]));
	}
}
