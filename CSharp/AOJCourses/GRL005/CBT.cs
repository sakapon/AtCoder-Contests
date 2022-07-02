using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;
using CoderLib8.Graphs.Arrays;

class CBT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToList());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var tree = new Tree(n, 0, map);
		var st = new MergeSBT<int>(tree.Tour.Count, new Monoid<int>((x, y) => tree.Depths[x] <= tree.Depths[y] ? x : y), tree.Tour.ToArray());

		return string.Join("\n", qs.Select(q =>
		{
			var u = q[0];
			var v = q[1];

			if (u == v) return u;
			if (tree.TourMap[u][0] > tree.TourMap[v][0]) { var t = u; u = v; v = t; }
			if (tree.TourMap[u].Last() > tree.TourMap[v].Last()) return u;
			return st[tree.TourMap[u].Last(), tree.TourMap[v][0]];
		}));
	}
}
