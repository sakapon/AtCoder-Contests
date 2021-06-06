using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.Spp;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var map = GraphConsole.ReadUnweightedMap(n + 1, m, true);

		var r = 0;

		for (int v = 1; v <= n; v++)
		{
			var result = map.Bfs(v);
			r += result.RawCosts.Count(x => x != long.MaxValue);
		}

		return r;
	}
}
