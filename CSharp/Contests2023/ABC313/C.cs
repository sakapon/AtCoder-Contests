using System;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		Array.Sort(a);
		if (a[0] == a[^1]) return 0;

		var b = First(0, 1 << 30, x => a.TakeWhile(v => v <= x).Sum(v => x - v) >= a.SkipWhile(v => v <= x).Sum(v => v - x - 1));

		var r1 = Math.Max(a.TakeWhile(v => v <= b).Sum(v => b - v), a.SkipWhile(v => v <= b).Sum(v => v - b - 1));
		b--;
		var r0 = Math.Max(a.TakeWhile(v => v <= b).Sum(v => b - v), a.SkipWhile(v => v <= b).Sum(v => v - b - 1));
		return Math.Min(r0, r1);
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
