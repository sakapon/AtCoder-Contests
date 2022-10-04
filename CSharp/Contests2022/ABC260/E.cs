using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		// a の値をキャッシュします。
		var q = Array.ConvertAll(new bool[m + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			var (a, b) = ps[i];
			q[a].Add(-1);
			q[b].Add(a);
		}

		var set = new WBMultiSet<int>();
		var raq = new StaticRAQ1(m + 1);

		for (int j = 1; j <= m; j++)
		{
			foreach (var a in q[j])
			{
				if (a != -1) set.Remove(a);
				set.Add(j);
			}

			if (set.Count < n) continue;
			raq.Add(j - set.GetFirst().Item + 1, j + 1, 1);
		}

		var sum = raq.GetSum();
		return string.Join(" ", sum[1..]);
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
