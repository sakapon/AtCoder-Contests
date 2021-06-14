using System;

namespace CoderLib8.Extra
{
	public static class SequenceHelper
	{
		// Longest Increasing Subsequence
		// 部分列の i 番目となりうる最小値 (0-indexed)。
		public static int[] Lis(int[] a)
		{
			var n = a.Length;
			var r = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);
			for (int i = 0; i < n; ++i)
				r[Min(0, n, x => r[x] >= a[i])] = a[i];
			return r;

			// 最大長を求める場合。
			//return Array.IndexOf(r, int.MaxValue);
		}

		static int Min(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
	}
}
