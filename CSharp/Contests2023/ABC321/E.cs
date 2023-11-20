using System;
using System.Linq;
using Range = System.ValueTuple<long, long>;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(string.Join("\n", new bool[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, x, kl) = Read3L();

		if (kl == 0) return 1;
		if (kl > 1 << 8) return 0;
		var k = (int)kl;

		var rn = new Range(1, n + 1);
		var s = 0L;

		// 頂点 n は dn 段目
		var dn = 0;
		for (int i = 0; i < 60; i++)
			if ((n & (1L << i)) != 0) dn = i;

		// 頂点 x は dx 段目
		var dx = 0;
		for (int i = 0; i < 60; i++)
			if ((x & (1L << i)) != 0) dx = i;

		var d_min = Math.Max(dx - k, 0);
		var d_max = Math.Min(dx + k, dn);

		for (int d = d_min; d <= d_max; d++)
		{
			if ((dx + k - d) % 2 == 1) continue;

			if (d == dx - k)
			{
				s += 1;
			}
			else if (d == dx + k)
			{
				s += SubtreeCount(x, k);
			}
			else
			{
				var dp = (dx + d - k) / 2;
				if (dp < 0) continue;
				var pv1 = x >> (dx - dp - 1);
				if ((pv1 & 1) == 0) pv1++;
				else pv1--;
				s += SubtreeCount(pv1, d - dp - 1);
			}
		}
		return s;

		// v からの距離が d の子孫の個数
		long SubtreeCount(long v, int d)
		{
			var l = v << d;
			var r = (v + 1) << d;
			(l, r) = Intersect(rn, new Range(l, r));
			return r - l;
		}
	}

	static Range Intersect(Range r1, Range r2)
	{
		var l = Math.Max(r1.Item1, r2.Item1);
		var r = Math.Min(r1.Item2, r2.Item2);
		if (l >= r) return new Range(0, 0);
		return new Range(l, r);
	}
}
