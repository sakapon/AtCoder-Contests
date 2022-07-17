using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var raq = new StaticRAQ1(200002);
		foreach (var (l, r) in ps)
			raq.Add(l, r, 1);
		var sum = raq.GetSum();

		var segs = Enumerable.Range(1, sum.Length - 1).Where(i => sum[i - 1] + sum[i] > 0 && sum[i - 1] * sum[i] == 0).ToArray();
		return string.Join("\n", Enumerable.Range(0, segs.Length / 2).Select(i => $"{segs[2 * i]} {segs[2 * i + 1]}"));
	}
}

public class StaticRAQ1
{
	int n;
	long[] d;
	public StaticRAQ1(int _n) { n = _n; d = new long[n]; }

	// O(1)
	// [l, r)
	// 範囲外のインデックスも可。
	public void Add(int l, int r, long v)
	{
		if (r < 0 || n <= l) return;
		d[Math.Max(0, l)] += v;
		if (r < n) d[r] -= v;
	}

	// O(n)
	public long[] GetSum()
	{
		var a = new long[n];
		a[0] = d[0];
		for (int i = 1; i < n; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}

	// O(n)
	// d をそのまま使います。
	public long[] GetSum0()
	{
		for (int i = 1; i < n; ++i) d[i] += d[i - 1];
		return d;
	}
}
