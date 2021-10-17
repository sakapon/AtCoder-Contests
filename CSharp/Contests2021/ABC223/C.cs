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

		var rsq = new StaticRSQ1(Array.ConvertAll(ps, p => p.a));

		var ts = Array.ConvertAll(ps, p => (double)p.a / p.b);
		var tSum = ts.Sum();
		var t2 = tSum / 2;

		var sum = 0.0;

		for (int i = 0; i < n; i++)
		{
			var t = sum + ts[i];
			if (t2 < t)
			{
				double r = rsq.GetSum(0, i);

				var (a, b) = ps[i];
				var d = t2 - sum;
				r += b * d;

				return r;
			}
			sum = t;
		}
		return -1;
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(int[] a)
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
