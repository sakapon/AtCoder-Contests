using Bang.Graphs.Int.SPPs.Unweighted.v1_0_2;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var sv = 1;
		var ev = n;
		var graph = new ListUnweightedGraph(n + 1, es, true);

		var dp = Array.ConvertAll(new bool[graph.VertexesCount], _ => -1);
		dp[sv] = 1;
		var q = new SortedSet<(int, int)> { (a[sv - 1], sv) };

		while (q.Count > 0)
		{
			var (h, v) = q.Min;
			q.Remove((h, v));
			if (v == ev) return dp[v];

			foreach (var nv in graph.GetEdges(v))
			{
				if (a[nv - 1] < h) continue;

				var nc = a[nv - 1] == h ? dp[v] : dp[v] + 1;
				if (dp[nv] < nc)
				{
					dp[nv] = nc;
					q.Add((a[nv - 1], nv));
				}
			}
		}
		return 0;
	}
}
