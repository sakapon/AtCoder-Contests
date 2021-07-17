using System;
using System.Collections.Generic;
using CoderLib8.Graphs.Arrays;

class Q039
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var tree = new Tree(n + 1, 1, es);
		var r = 0L;

		foreach (var e in es)
		{
			var v = tree.Depths[e[0]] > tree.Depths[e[1]] ? e[0] : e[1];

			var c = (tree.TourMap[v][^1] - tree.TourMap[v][0]) / 2 + 1L;
			r += c * (n - c);
		}
		return r;
	}
}
