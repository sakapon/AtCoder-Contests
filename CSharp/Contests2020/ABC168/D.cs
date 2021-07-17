using System;
using System.Collections.Generic;
using Bang.Graphs.Int.Spp;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var map = GraphConsole.ReadUnweightedMap(n + 1, m, false);

		var r = map.Bfs(1);
		Console.WriteLine("Yes");
		Console.WriteLine(string.Join("\n", r.RawInVertexes[2..]));
	}
}
