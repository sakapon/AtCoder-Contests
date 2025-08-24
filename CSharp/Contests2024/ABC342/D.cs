class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var cs = new long[200001];

		foreach (var x in a)
		{
			if (x == 0) { cs[0]++; continue; }

			var y = 1L;
			foreach (var g in Factorize(x).GroupBy(p => p))
			{
				if (g.Count() % 2 == 1)
				{
					y *= g.Key;
				}
			}
			cs[y]++;
		}

		var r = Enumerable.Range(1, (int)cs[0]).Sum(i => (long)n - i);

		for (int i = 1; i <= 200000; i++)
		{
			r += cs[i] * (cs[i] - 1) / 2;
		}
		return r;
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
