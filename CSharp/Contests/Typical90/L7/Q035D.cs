using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Arrays;

class Q035D
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
			var k = vs.Length;

			var r = 0;
			for (int i = 0; i < k; i++)
			{
				r += bll.GetDistance(vs[i], vs[(i + 1) % k]);
			}
			Console.WriteLine(r / 2);
		}
		Console.Out.Flush();
	}
}
