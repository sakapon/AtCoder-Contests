class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var s = new List<long>();
		var t = a.Sum();

		var n2 = (n + 1) / 2;
		for (int i = 0; i < n2; i++)
		{
			var ps = s.Count == 0 ? 0L : s[^1];
			s.Add((ps + t) % M);

			t -= a[i];
			t -= a[n - 1 - i];
		}

		s.AddRange(s.Take(n / 2).Reverse().ToArray());
		return s.Select((x, i) => x * MInv(i + 1) % M).Sum() % M;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
}
