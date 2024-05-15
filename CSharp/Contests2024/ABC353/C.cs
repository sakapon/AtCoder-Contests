class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		const long M = 100000000;

		Array.Sort(a);
		var r = (n - 1) * a.Sum();

		for (int i = 0; i < n; i++)
		{
			var v = a[i];
			var j = First(i + 1, n, k => a[k] + v >= M);
			r -= (n - j) * M;
		}
		return r;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
