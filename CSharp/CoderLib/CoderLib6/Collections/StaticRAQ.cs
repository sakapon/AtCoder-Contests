using System;

namespace CoderLib6.Collections
{
	// いもす法
	// 範囲に対する加算クエリを一括で処理します。
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/5/DSL_5_A
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

	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/5/DSL_5_B
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ab
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_cc
	public class StaticRAQ2
	{
		int n1, n2;
		long[,] d;
		public StaticRAQ2(int _n1, int _n2) { n1 = _n1; n2 = _n2; d = new long[n1, n2]; }

		// O(1)
		// 範囲外のインデックスも可。
		public void Add(int l1, int l2, int r1, int r2, long v)
		{
			if (r1 < 0 || n1 <= l1) return;
			if (r2 < 0 || n2 <= l2) return;
			d[Math.Max(0, l1), Math.Max(0, l2)] += v;
			if (r2 < n2) d[Math.Max(0, l1), r2] -= v;
			if (r1 < n1) d[r1, Math.Max(0, l2)] -= v;
			if (r1 < n1 && r2 < n2) d[r1, r2] += v;
		}

		// O(n1 n2)
		public long[,] GetSum()
		{
			var a = new long[n1, n2];
			for (int i = 0; i < n1; ++i) a[i, 0] = d[i, 0];
			for (int i = 0; i < n1; ++i)
				for (int j = 1; j < n2; ++j) a[i, j] = a[i, j - 1] + d[i, j];
			for (int j = 0; j < n2; ++j)
				for (int i = 1; i < n1; ++i) a[i, j] += a[i - 1, j];
			return a;
		}

		// O(n1 n2)
		// d をそのまま使います。
		public long[,] GetSum0()
		{
			for (int i = 0; i < n1; ++i)
				for (int j = 1; j < n2; ++j) d[i, j] += d[i, j - 1];
			for (int j = 0; j < n2; ++j)
				for (int i = 1; i < n1; ++i) d[i, j] += d[i - 1, j];
			return d;
		}
	}
}
