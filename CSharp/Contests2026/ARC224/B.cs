class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var n2 = Last(1, 1 << 30, x => x * x <= n);

		var r = n2 * (n2 - 1) * 2;
		var rem = n - n2 * n2;
		if (rem > n2)
		{
			r += n2 + n2 - 1;
			rem -= n2;
		}
		if (rem > 0)
		{
			r += rem + rem - 1;
		}
		return r;
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
