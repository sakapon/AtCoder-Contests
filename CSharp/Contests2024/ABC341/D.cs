class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3L();
		k--;

		var g = Gcd(n, m);
		n /= g;
		m /= g;
		var nm = n * m;

		var period = n + m - 2;
		var (q, rem) = Math.DivRem(k, period);

		var y = First(0, nm, x => x / n + x / m >= rem + 1);
		return g * (q * nm + y);
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
