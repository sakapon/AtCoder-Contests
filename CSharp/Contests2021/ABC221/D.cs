using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var xs = ps.SelectMany(p => new[] { p.a, p.a + p.b }).ToArray();
		var map = new CompressionHashMap(xs);
		var rev = map.ReverseMap;

		var raq = new StaticRAQ1(map.Count);

		foreach (var (a, b) in ps)
		{
			raq.Add(map[a], map[a + b], 1);
		}
		var s = raq.GetSum();

		var r = new long[n + 1];

		for (int i = 0; i < s.Length - 1; i++)
		{
			r[s[i]] += rev[i + 1] - rev[i];
		}

		return string.Join(" ", r[1..]);
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
