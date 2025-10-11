class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m, k) = Read3();
		var s = Console.ReadLine();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var (u, v) in es)
			map[u - 1].Add(v - 1);

		var wins = s.Select(c => c == 'B').ToArray();
		var wins_t = new bool[n];

		while (k-- > 0)
		{
			for (int u = 0; u < n; u++)
				wins_t[u] = map[u].Any(v => wins[v]);
			(wins, wins_t) = (wins_t, wins);

			for (int u = 0; u < n; u++)
				wins_t[u] = map[u].All(v => wins[v]);
			(wins, wins_t) = (wins_t, wins);
		}

		return wins[0] ? "Bob" : "Alice";
	}
}
