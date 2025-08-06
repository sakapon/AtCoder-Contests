class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		var dp = new int[n + 1, 1001];
		for (var i = 0; i <= n; i++)
			for (var j = 0; j <= 1000; j++)
				dp[i, j] = -1;

		int Get(int i, int j)
		{
			if (dp[i, j] != -1) return dp[i, j];

			var (p, a, b) = ps[i - 1];
			var nj = p >= j ? j + a : j - b;
			if (nj < 0) nj = 0;

			if (i == n)
				return dp[i, j] = nj;
			else
				return dp[i, j] = Get(i + 1, nj);
		}

		var s = new int[n + 1];
		for (int i = 0; i < n; i++)
			s[i + 1] = s[i] + ps[i].Item3;

		return string.Join("\n", qs.Select(x =>
		{
			if (x <= 1000)
			{
				return Get(1, x);
			}
			else
			{
				var xi = First(0, n + 1, i => x - s[i] <= 1000);
				if (xi >= n) return x - s[n];
				return Get(1 + xi, x - s[xi]);
			}
		}));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
