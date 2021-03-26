using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Collections
{
	// Test: https://codeforces.com/contest/1501/problem/B
	public class StaticRangeUnion
	{
		// 値の範囲が小さい場合に使います (0 <= x <= 10^7)。
		public static bool[] Tally((int l_in, int r_ex)[] ranges, int max)
		{
			// ソート済の場合は不要です。
			ranges = ranges.OrderBy(t => t.l_in).ToArray();

			var b = new bool[max + 1];
			var i = 0;
			foreach (var (l, r) in ranges)
				for (i = Math.Max(i, l); i < r && i <= max; ++i)
					b[i] = true;
			return b;
		}

		List<(int l, int r)> rs = new List<(int l, int r)>();

		// 値の範囲を問いません。
		public StaticRangeUnion((int l_in, int r_ex)[] ranges)
		{
			// ソート済の場合は不要です。
			ranges = ranges.OrderBy(t => t.l_in).ToArray();

			foreach (var (l, r) in ranges)
			{
				if (l >= r) continue;
				if (rs.Count == 0)
				{
					rs.Add((l, r));
					continue;
				}

				var (l0, r0) = rs[rs.Count - 1];
				if (r0 < l)
					rs.Add((l, r));
				else if (r0 < r)
					rs[rs.Count - 1] = (l0, r);
			}
		}

		public bool Contains(int x)
		{
			var i = First(0, rs.Count, j => x < rs[j].l);
			return i > 0 && x < rs[i - 1].r;
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
