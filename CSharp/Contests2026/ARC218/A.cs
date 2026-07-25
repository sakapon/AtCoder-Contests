class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read());

		var counts = Array.ConvertAll(new bool[n * m + 1], _ => new int[n]);

		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				counts[a[i][j]][i]++;
			}

		var r = MPow(m, n) * n * m % M;

		for (int v = 1; v <= n * m; v++)
		{
			var p = counts[v].Select(c => (long)m - c).Aggregate((x, y) => x * y % M);
			r += M - p;
			r %= M;
		}
		return r;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
