using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = ReadL();

		return Max(0, 1L << 60, x => a.Sum(v => Math.Min(x, v)) / k >= x);
	}

	static long Max(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
