class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = new SeqGrid<int>(Array.ConvertAll(new bool[h], _ => Read()));
		var p = Read();

		var dp = new SeqGrid<long>(h + 1, w + 1);
		Point ep = (h - 1, w - 1);
		return First(0, 1L << 50, Check);

		bool Check(long x)
		{
			dp.Fill(long.MinValue);
			dp[0, 0] = x;

			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					if (dp[i, j] < 0) continue;

					dp[i, j] += a[i, j] - p[i + j];
					if (dp[i, j] < 0) continue;

					dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
					dp[i, j + 1] = Math.Max(dp[i, j + 1], dp[i, j]);
				}
			return dp[ep] >= 0;
		}
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
