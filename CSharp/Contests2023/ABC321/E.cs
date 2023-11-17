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

		if (kl > 1 << 8) return 0;
		var k = (int)kl;
		if (k == 0) return 1;

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

		for (int i = 0; i < dn; i++)
		{
			if (dx - k <= i && i <= dx + k)
			{
				if (i == dx - k)
				{
					s += 1;
				}
				else if (i == dx + k)
				{
					s += 1L << k;
				}
				else if ((dx + i - k) % 2 == 0)
				{
					var p = (dx + i - k) / 2;
					if (p >= 0)
					{
						s += 1L << (i - p - 1);
					}
				}
			}
		}

		if (dx - k <= dn && dn <= dx + k)
		{
			if (dn == dx + k)
			{
				s += SubtreeCount(x, k);
			}
			else if ((dx + dn - k) % 2 == 0)
			{
				var p = (dx + dn - k) / 2;
				if (p >= 0)
				{
					var pv1 = x >> (dx - p - 1);
					if ((pv1 & 1) == 0) pv1++;
					else pv1--;
					s += SubtreeCount(pv1, dn - p - 1);
				}
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
