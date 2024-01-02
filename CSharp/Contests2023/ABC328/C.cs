using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var a = Array2.Range(0, n - 1)
			.Select(i => s[i] == s[i + 1] ? 1 : 0L);
		var rsq = new StaticRSQ1(a);

		var res = qs.Select(q => rsq.GetSum(q.l - 1, q.r - 1));
		return string.Join("\n", res);
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
