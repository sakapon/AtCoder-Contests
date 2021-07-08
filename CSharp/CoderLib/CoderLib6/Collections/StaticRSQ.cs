using System;

namespace CoderLib6.Collections
{
	// 累積和
	// 範囲に対する和の取得クエリを一括で処理します。
	// Test: https://judge.yosupo.jp/problem/static_range_sum
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_j
	public class StaticRSQ1
	{
		long[] s;
		public StaticRSQ1(int[] a)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}

		// [l, r)
		public long GetSum(int l, int r) => s[r] - s[l];
	}
	// 重めの処理です。
	//public long GetSum(Range r) => GetSum(r.Start.GetOffset(s.Length - 1), r.End.GetOffset(s.Length - 1));

	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_cc
	public class StaticRSQ2
	{
		long[,] s;
		public StaticRSQ2(int[,] a)
		{
			var n1 = a.GetLength(0);
			var n2 = a.GetLength(1);
			s = new long[n1 + 1, n2 + 1];
			for (int i = 0; i < n1; ++i)
				for (int j = 0; j < n2; j++)
					s[i + 1, j + 1] = s[i + 1, j] + a[i, j];
			for (int i = 1; i < n1; ++i)
				for (int j = 1; j <= n2; j++)
					s[i + 1, j] += s[i, j];
		}

		public long GetSum(int l1, int l2, int r1, int r2) => s[r1, r2] - s[l1, r2] - s[r1, l2] + s[l1, l2];
	}
}
