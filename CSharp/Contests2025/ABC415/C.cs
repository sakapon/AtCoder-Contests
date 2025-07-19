using CoderLib8.Graphs.SPPs.Int.UnweightedGraph211;

class C
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve() ? "Yes" : "No")));
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = ("0" + Console.ReadLine()).Select(c => c == '1').ToArray();

		var g = new UnweightedGraph(1 << n);

		for (int x = 0; x < 1 << n; x++)
		{
			if (s[x]) continue;

			for (int i = 0; i < n; i++)
			{
				var nx = x | (1 << i);
				if (nx == x) continue;
				if (s[nx]) continue;
				g.AddEdge(x, nx, false);
			}
		}

		var ev = g.VertexesCount - 1;
		g.ShortestByBFS(0, ev);
		return g[ev].IsConnected;
	}
}
