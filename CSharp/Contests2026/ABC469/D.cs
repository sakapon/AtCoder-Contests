class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var counts = new int[n];
		var counts2 = new Dictionary<(int, int), int>();

		foreach (var e in es)
		{
			var (u, v) = e;
			u--;
			v--;

			counts[u]++;
			counts[v]++;
			counts2[(u, v)] = counts2.GetValueOrDefault((u, v)) + 1;
			counts2[(v, u)] = counts2.GetValueOrDefault((v, u)) + 1;
		}

		var ordered = Enumerable.Range(0, n)
			.Select(v => (v, c: counts[v]))
			.OrderBy(g => -g.c)
			.ToArray();

		var r = 0;

		for (int i = 0; i < n; i++)
		{
			var (v, c) = ordered[i];
			if (c < (m + 1) / 2) break;

			for (int j = i + 1; j < n; j++)
			{
				if (counts[v] + counts[ordered[j].v] - counts2.GetValueOrDefault((v, ordered[j].v)) == m)
					r++;
			}
		}
		return r;
	}
}
