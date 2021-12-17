using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();
		var a = Read();

		var s = CumSumL(a);
		Array.Sort(s);

		return First(0, 1L << 60, x =>
		{
			var c = 0L;

			foreach (var (i, j) in TwoPointers((int)n, (int)n + 2, (i, j) => j == n + 1 || s[j] - s[i] > x))
			{
				c += j - i - 1;
			}
			return c >= k;
		});
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
