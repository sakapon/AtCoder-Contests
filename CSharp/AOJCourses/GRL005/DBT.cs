using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees.SBTs;
using CoderLib8.Graphs.Int;

class DBT
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = Read()[0];
		var map = Array.ConvertAll(new bool[n], _ => Read().Skip(1).ToList());
		var qc = Read()[0];
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var graph = new UGraph(n, map);
		var tree = new UTree(graph, 0);
		var st = new MergeSBT<long>(2 * n, Monoid.Int64_Add);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			if (q[0] == 0)
			{
				var (v, w) = (q[1], q[2]);
				var (si, ei) = (tree.Nodes[v].Orders[0], tree.Nodes[v].Orders.Last());
				st[si] += w;
				st[ei + 1] -= w;
			}
			else
			{
				var u = q[1];
				var si = tree.Nodes[u].Orders[0];
				Console.WriteLine(st[1, si + 1]);
			}
		}
		Console.Out.Flush();
	}
}
