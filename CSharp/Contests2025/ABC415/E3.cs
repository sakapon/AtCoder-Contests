class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = new SeqGrid<int>(Array.ConvertAll(new bool[h], _ => Read()));
		var p = Read();

		var dp = new SeqGrid<long>(h, w, long.MaxValue);
		dp[h - 1, w - 1] = 0;

		for (int i = h - 1; i >= 0; i--)
			for (int j = w - 1; j >= 0; j--)
			{
				var nv = dp[i, j] + p[i + j] - a[i, j];
				nv.Chmax(0);
				if (dp.IsInside(i - 1, j)) dp[i - 1, j].Chmin(nv);
				if (dp.IsInside(i, j - 1)) dp[i, j - 1].Chmin(nv);
			}
		var r = dp[0, 0] + p[0] - a[0, 0];
		return r.Chmax(0);
	}
}
