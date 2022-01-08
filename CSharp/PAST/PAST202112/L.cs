using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p) = Read2();
		var a = Read();

		var max = p - n + 1;
		Array.Reverse(a);
		a = a.Select((x, i) => x - i).Where(x => 0 <= x && x <= max).ToArray();
		return n - Lis(a);
	}

	public static int Lis(int[] a)
	{
		var n = a.Length;
		var r = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);
		for (int i = 0; i < n; ++i)
			r[Min(0, n, x => r[x] > a[i])] = a[i];
		return Array.IndexOf(r, int.MaxValue);
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
