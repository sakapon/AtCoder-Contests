class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long a, long b) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2L();
		var ps = Array.ConvertAll(new bool[m], _ => Read2L());

		var r = 0L;
		foreach (var (a, b) in ps.OrderBy(p => p.a - p.b))
		{
			var d = a - b;
			if (n < a) continue;
			var c = (n - a) / d + 1;
			r += c;
			n -= d * c;
		}
		return r;
	}
}
