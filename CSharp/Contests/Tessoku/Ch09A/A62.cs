using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Arrays.PathCore111;

class A62
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = PathCore.ToMap(n + 1, es, true);
		var r = map.ConnectivityByDFS(1);
		return $"The graph is {(r[1..].All(b => b) ? "" : "not ")}connected.";
	}
}
