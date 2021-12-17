using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var x = long.Parse(Console.ReadLine());

		var rsq = new StaticRSQ1(a);

		return First(0, 1L << 60, k =>
		{
			BigInteger q = Math.DivRem(k, n, out var r);
			return rsq.GetSum(0, n) * q + rsq.GetSum(0, (int)r) > x;
		});
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
