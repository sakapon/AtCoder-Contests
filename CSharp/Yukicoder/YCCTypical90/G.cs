using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = ((int, long, int))Read3L();
		var a = Read();
		var b = Read();

		Array.Sort(a);
		Array.Reverse(a);
		Array.Sort(b);
		b = b.Concat(b.Select(v => v + p)).Concat(b.Select(v => v + 2 * p)).ToArray();

		return First(0, p, x =>
		{
			// x までの個数
			var c = 0L;
			var l = new int[n];
			var r = new int[n];

			foreach (var (i, j) in TwoPointers(n, b.Length, (i, j) => a[i] + b[j] >= p))
				l[i] = j;
			foreach (var (i, j) in TwoPointers(n, b.Length, (i, j) => a[i] + b[j] > p + x))
				r[i] = j;

			for (int i = 0; i < n; i++)
				c += r[i] - l[i];
			return c >= k;
		});
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
