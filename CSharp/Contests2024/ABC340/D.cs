using Bang.Graphs.Int.SPPs.Weighted.v1_0_2;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n - 1], _ => Read3());

		var g = new ListWeightedGraph(n + 1);

		for (int i = 1; i < n; i++)
		{
			var (a, b, x) = ps[i - 1];

			g.AddEdge(i, i + 1, false, a);
			g.AddEdge(i, x, false, b);
		}

		var r = g.Dijkstra(1, n);
		return r[n];
	}
}
