class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var (u, v) in es)
		{
			if (a[u - 1] <= a[v - 1]) map[u].Add(v);
			if (a[u - 1] >= a[v - 1]) map[v].Add(u);
		}

		var sv = 1;
		var ev = n;

		var dp = new int[n + 1];
		dp[sv] = 1;
		var q = new SortedSet<(int, int)> { (a[sv - 1], sv) };

		while (q.Count > 0)
		{
			var (h, v) = q.Min;
			q.Remove((h, v));
			if (v == ev) break;

			foreach (var nv in map[v])
			{
				var nc = a[nv - 1] == h ? dp[v] : dp[v] + 1;
				if (dp[nv] >= nc) continue;
				dp[nv] = nc;
				q.Add((a[nv - 1], nv));
			}
		}
		return dp[ev];
	}
}
