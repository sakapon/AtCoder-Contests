class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var n = h * w;
		var a = Array.ConvertAll(new bool[h], _ => ReadL());
		var a2 = a.SelectMany(v => v).ToArray();

		var pairs = new List<(int, int)>();
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				var v = w * i + j;

				if (i != 0)
					pairs.Add((v - w, v));
				if (j != 0)
					pairs.Add((v - 1, v));
			}

		var dp = new long[1 << n];
		Array.Fill(dp, -1);
		dp[0] = a2.Aggregate((x, y) => x ^ y);
		var r = dp[0];

		for (int x = 0; x < 1 << n; x++)
		{
			if (dp[x] == -1) continue;

			foreach (var (u, v) in pairs)
			{
				if ((x & (1 << u)) != 0 || (x & (1 << v)) != 0) continue;
				var nx = x | (1 << u) | (1 << v);
				dp[nx] = dp[x] ^ a2[u] ^ a2[v];
				r = Math.Max(r, dp[nx]);
			}
		}
		return r;
	}
}
