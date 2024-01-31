using System;
using CoderLib6.Graphs;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var h = Read();
		int n = h[0];
		var es = Array.ConvertAll(new int[h[1]], _ => ReadL());

		var d = TSP.ToAdjacencyMatrix(n, es);

		var sv = 0;
		var dp = TSP.Execute(n, sv, d);
		var r = dp[(1 << n) - 1][sv];
		if (r == long.MaxValue) return -1;
		return r;
	}
}
