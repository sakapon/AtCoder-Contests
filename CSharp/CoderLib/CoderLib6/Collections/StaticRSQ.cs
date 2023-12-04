using System;

namespace CoderLib6.Collections
{
	// 累積和
	// 範囲に対する和の取得クエリを一括で処理します。
	// Test: https://judge.yosupo.jp/problem/static_range_sum
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_j
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bx
	public class StaticRSQ1
	{
		int n;
		long[] s;
		public long[] Raw => s;
		public StaticRSQ1(long[] a)
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
	// 重めの処理です。
	//public long GetSum(Range r) => GetSum(r.Start.GetOffset(s.Length - 1), r.End.GetOffset(s.Length - 1));

	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_cc
	// Test: https://atcoder.jp/contests/abc331/tasks/abc331_d
	public class StaticRSQ2
	{
		long[,] s;
		public StaticRSQ2(int[,] a)
		{
			var n1 = a.GetLength(0);
			var n2 = a.GetLength(1);
			s = new long[n1 + 1, n2 + 1];
			for (int i = 0; i < n1; ++i)
			{
				for (int j = 0; j < n2; ++j) s[i + 1, j + 1] = s[i + 1, j] + a[i, j];
				for (int j = 1; j <= n2; ++j) s[i + 1, j] += s[i, j];
			}
		}

		public long GetSum(int l1, int l2, int r1, int r2) => s[r1, r2] - s[l1, r2] - s[r1, l2] + s[l1, l2];
	}
}
