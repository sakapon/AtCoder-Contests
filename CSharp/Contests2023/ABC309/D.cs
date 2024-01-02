using System;
using System.Collections.Generic;
using System.Linq;
using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n1, n2, m) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var g = new ListUnweightedGraph(n1 + n2 + 1, es, true);
		var r1 = g.ShortestByBFS(1);
		var r2 = g.ShortestByBFS(n1 + n2);
		return r1.Max(d => d < long.MaxValue ? d : -1) + r2.Max(d => d < long.MaxValue ? d : -1) + 1;
	}
}
