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
		l = UpperMultiple(l);
		r = LowerMultiple(r);
		l /= M;
		r /= M;
		return r - l + 1;

		long MInt(long x) => (x %= M) < 0 ? x + M : x;
		long LowerMultiple(long x) => x - MInt(x);
		long UpperMultiple(long x) { var r = MInt(x); return r == 0 ? x : x + M - r; }
	}
}
