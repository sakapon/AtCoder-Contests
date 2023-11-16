using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Linq;
using Num = System.Int64;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, p) = ReadL().ToTuple3<int, int, long>();
		var a = ReadL();
		var b = ReadL();

		Array.Sort(a);
		var rsq = new StaticRSQ1(a);

		return b.Sum(bx =>
		{
			var ai = a.FirstIndexByBS(ax => ax >= p - bx);
			return rsq.GetSum(0, ai) + ai * bx + (n - ai) * p;
		});
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
