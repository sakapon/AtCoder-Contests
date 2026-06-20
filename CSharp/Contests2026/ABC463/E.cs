using Bang.Graphs.Int.SPPs.Weighted.v1_0_2;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, y) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read3());
		var x = Read();

		var g = new ListWeightedGraph(n + 3, es, true);
		g.AddEdge(n + 1, n + 2, false, y);

		for (int v = 1; v <= n; v++)
		{
			g.AddEdge(v, n + 1, false, x[v - 1]);
			g.AddEdge(n + 2, v, false, x[v - 1]);
		}

		var r = g.Dijkstra(1);
		return string.Join(" ", r.Skip(2).Take(n - 1));
	}
}
