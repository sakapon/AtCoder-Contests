using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	const long max = 1L << 60;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int p, int t) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k, q) = Read4();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var p0 = ps.Where(p => p.t == 0).Select(p => p.p).OrderBy(x => x).ToArray();
		var p1 = ps.Where(p => p.t == 1).Select(p => p.p).OrderBy(x => x).ToArray();

		var cs0 = CumSumL(p0);
		var cs1 = CumSumL(p1);

		// x1: 缶切りが必要な商品の数
		long Sum(int x1)
		{
			var x0 = m - x1;
			if (x0 > p0.Length || x1 > p1.Length) return max;
			return cs0[x0] + cs1[x1] + (long)(x1 + k - 1) / k * q;
		}
		return Enumerable.Range(0, m + 1).Min(Sum);
	}

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
