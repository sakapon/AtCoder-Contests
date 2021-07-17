using System;

namespace CoderLib8.Collections
{
	// Test: https://codeforces.com/contest/1480/problem/C
	static class TernarySearch
	{
		// 解の候補が [1, 5] の場合、l = 0, r = 6 を指定します。
		// f: (inside, outside) => isInsideMore
		// 局所最小の例:
		// ArgTrue(0, n + 1, (i, o) => Get(i) <= Get(o))
		static int ArgTrue(int l, int r, Func<int, int, bool> f)
		{
			var m = l + (r - l) / 2;
			int t;

			while (m - l > 1 || r - m > 1)
				if (m - l >= r - m)
				{
					if (f(t = m - (m - l) / 2, m)) (m, r) = (t, m);
					else l = t;
				}
				else
				{
					if (f(t = m + (r - m) / 2, m)) (m, l) = (t, m);
					else r = t;
				}
			return m;
		}
	}
}
