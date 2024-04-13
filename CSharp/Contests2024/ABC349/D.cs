class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (l, r) = Read2L();

		var l1 = new List<(long l, long r)>();
		var l2 = new List<(long l, long r)>();

		for (var f = 1L; l < r; f <<= 1)
		{
			if ((l & f) != 0)
			{
				l1.Add((l, l + f));
				l += f;
			}

			if (l == r) break;

			if ((r & f) != 0)
			{
				l2.Add((r - f, r));
				r -= f;
			}
		}

		l2.Reverse();
		l1.AddRange(l2);
		return $"{l1.Count}\n" + string.Join("\n", l1.Select(p => $"{p.l} {p.r}"));
	}
}
