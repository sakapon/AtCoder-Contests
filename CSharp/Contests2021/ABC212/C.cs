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
		var (n, m) = Read2();
		var a = Read();
		var b = ReadL().Append(-1L << 40).Append(1L << 40).ToArray();

		Array.Sort(b);

		return a.Min(v =>
		{
			var i = Min(0, b.Length, x => b[x] >= v);
			return Math.Min(Math.Abs(v - b[i]), Math.Abs(v - b[i - 1]));
		});
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
