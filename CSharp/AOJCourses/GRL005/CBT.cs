using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;
using CoderLib8.Graphs.Int;

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

		var graph = new UGraph(n, map);
		var tree = new UTree(graph, 0);
		var st = new MergeSBT<UTree.Node>(tree.TourNodes.Length, new Monoid<UTree.Node>((x, y) => x.Depth <= y.Depth ? x : y), tree.TourNodes);

		return string.Join("\n", qs.Select(q =>
		{
			var u = q[0];
			var v = q[1];

			if (u == v) return u;
			if (tree.Nodes[u].Orders[0] > tree.Nodes[v].Orders[0]) { var t = u; u = v; v = t; }
			if (tree.Nodes[u].Orders.Last() > tree.Nodes[v].Orders.Last()) return u;
			return st[tree.Nodes[u].Orders.Last(), tree.Nodes[v].Orders[0]].Id;
		}));
	}
}
