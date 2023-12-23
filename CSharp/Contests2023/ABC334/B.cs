class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, M, l, r) = Read4L();

		l -= a;
		r -= a;

		if (l % M != 0) l += M - MInt(l);
		r -= MInt(r);
		l /= M;
		r /= M;
		return r - l + 1;

		long MInt(long x) => (x %= M) < 0 ? x + M : x;
	}
}
