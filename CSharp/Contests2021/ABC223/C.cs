using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ts = Array.ConvertAll(ps, p => (double)p.a / p.b);
		var rsq = new StaticRSQ1(ts);
		var t2 = ts.Sum() / 2;

		var si = First(0, n + 1, x => rsq.GetSum(0, x) >= t2);
		var dt = rsq.GetSum(0, si) - t2;
		return Enumerable.Range(0, si).Sum(i => ps[i].a) - ps[si - 1].b * dt;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

public class StaticRSQ1
{
	int n;
	double[] s;
	public double[] Raw => s;
	public StaticRSQ1(double[] a)
	{
		n = a.Length;
		s = new double[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public double GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
