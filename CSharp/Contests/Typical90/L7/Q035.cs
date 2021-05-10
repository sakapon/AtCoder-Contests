using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Arrays;

class Q035
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qc = int.Parse(Console.ReadLine());

		var tree = new Tree(n + 1, 1, es);
		var bll = new BLLca(tree);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int qi = 0; qi < qc; qi++)
		{
			var vs = Read().Skip(1).OrderBy(v => tree.TourMap[v][0]).ToArray();

			var r = 0;
			var tv = bll.GetLca(vs[0], vs[^1]);

			foreach (var v in vs)
			{
				var lca = bll.GetLca(tv, v);
				r += tree.Depths[v] - tree.Depths[lca];
				tv = v;
			}
			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}
}
