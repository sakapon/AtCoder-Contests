using System;
using CoderLib6.Graphs;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var h = Read();
		int n = h[0];
		var es = Array.ConvertAll(new int[h[1]], _ => Read());

		var d = TSP.NewArray2(n, n, long.MaxValue);
		foreach (var e in es)
			d[e[0]][e[1]] = e[2];

		var sv = 0;
		var dp = TSP.Execute(n, sv, d);
		var r = dp[(1 << n) - 1][sv];
		if (r == long.MaxValue) return -1;
		return r;
	}
}
