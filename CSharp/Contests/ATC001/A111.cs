using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Arrays.PathCore111;

class A111
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var sseq = s.SelectMany(a => a).ToArray();
		var sv = Array.IndexOf(sseq, 's');
		var ev = Array.IndexOf(sseq, 'g');

		var grid = new GridHelper(h, w);
		var map = grid.GetAdjacencyList(s).ToArrays();
		var r = map.ConnectivityByDFS(sv, ev);
		return r[ev];
	}
}
