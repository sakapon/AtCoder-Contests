using System;
using System.Linq;

class D
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		long n = h[0], k = h[1];
		var a = Read().OrderBy(x => x).ToArray();

		Console.WriteLine(First(-1L << 60, 1L << 60, x => Enumerable.Range(0, (int)n - 1).Sum(i => a[i] >= 0 ? First(i + 1, n, j => a[i] * a[j] > x) - i - 1 : n - 1 - Last(i, n - 1, j => a[i] * a[j] > x)) >= k));
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
