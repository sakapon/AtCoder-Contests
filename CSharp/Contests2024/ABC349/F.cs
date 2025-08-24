class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2L();
		var a = ReadL();

		a = Array.FindAll(a, x => m % x == 0);
		var pows = MPows(2, a.Length);

		var ps = Factorize(m).GroupBy(p => p).Select(g => g.Aggregate((x, y) => x * y)).ToArray();
		var k = ps.Length;

		int Normalize(long x)
		{
			var r = 0;
			for (int i = 0; i < k; i++)
				if (x % ps[i] == 0) r |= 1 << i;
			return r;
		}
		var b = Array.ConvertAll(a, Normalize);

		var dp = new long[1 << k];
		dp[0] = 1;

		foreach (var g in b.GroupBy(x => x))
		{
			var x = g.Key;
			var c = g.Count();
			var p = (pows[c] - 1 + M) % M;

			for (int x0 = dp.Length - 1; x0 >= 0; x0--)
			{
				var nx = x0 | x;
				dp[nx] += dp[x0] * p;
				dp[nx] %= M;
			}
		}

		if (m == 1) return (dp[0] - 1 + M) % M;
		return dp[^1];
	}

	const long M = 998244353;
	static long[] MPows(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
		return p;
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
