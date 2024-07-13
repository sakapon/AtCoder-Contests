class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long l, long r) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var sMin = ps.Sum(p => p.l);
		var sMax = ps.Sum(p => p.r);

		if (sMin > 0 || sMax < 0) return "No";

		var x = ps.Select(p => p.l).ToArray();
		var d = ps.Select(p => p.r - p.l).ToArray();
		var e = -sMin;

		for (int i = 0; i < n; i++)
		{
			if (e == 0) break;

			var v = Math.Min(d[i], e);
			x[i] += v;
			e -= v;
		}

		return "Yes\n" + string.Join(" ", x);
	}
}
