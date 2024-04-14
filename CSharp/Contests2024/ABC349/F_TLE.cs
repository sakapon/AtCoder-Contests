class F_TLE
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
		a = Array.ConvertAll(a, x => ps.Select(p => x % p == 0 ? p : 1).Aggregate(1L, (x, y) => x * y));

		var dp = new Dictionary<long, long>();
		dp[1] = 1;

		foreach (var g in a.GroupBy(x => x))
		{
			var x = g.Key;
			var c = g.Count();
			var p = (pows[c] - 1 + M) % M;

			foreach (var (x0, v0) in dp.ToArray())
			{
				var nx = Lcm(x0, x);
				var nv = dp.GetValueOrDefault(nx, 0) + v0 * p;
				dp[nx] = nv % M;
			}
		}

		if (m == 1) return (dp[1] - 1 + M) % M;
		return dp.GetValueOrDefault(m, 0);
	}

	const long M = 998244353;
	static long[] MPows(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
		return p;
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }
	static long Lcm(long a, long b) => a / Gcd(a, b) * b;

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
