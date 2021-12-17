using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		Array.Sort(a);
		return string.Join("\n", qs.Select(GetCount));

		int GetCount(int x)
		{
			return n - First(0, n, i => a[i] >= x);
		}
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
