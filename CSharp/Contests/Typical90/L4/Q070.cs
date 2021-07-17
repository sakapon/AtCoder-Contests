using System;

class Q070
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var xs = Array.ConvertAll(ps, p => p.x);
		var ys = Array.ConvertAll(ps, p => p.y);
		Array.Sort(xs);
		Array.Sort(ys);

		var xs1 = CumSumL(xs);
		var ys1 = CumSumL(ys);
		Array.Reverse(xs);
		Array.Reverse(ys);
		var xs2 = CumSumL(xs);
		var ys2 = CumSumL(ys);
		Array.Reverse(xs);
		Array.Reverse(ys);

		var xr = 1L << 60;
		var yr = 1L << 60;

		for (int i = 0; i < n; i++)
		{
			xr = Math.Min(xr, (2 * i + 1 - n) * xs[i] - xs1[i] + xs2[n - 1 - i]);
			yr = Math.Min(yr, (2 * i + 1 - n) * ys[i] - ys1[i] + ys2[n - 1 - i]);
		}
		return xr + yr;
	}

	static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
