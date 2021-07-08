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
		// 範囲外のインデックスも可。
		public void Add(int l_in, int r_ex, long v)
		{
			if (r_ex < 0 || n <= l_in) return;
			d[Math.Max(0, l_in)] += v;
			if (r_ex < n) d[r_ex] -= v;
		}

		// O(n)
		public long[] GetAll()
		{
			var a = new long[n];
			a[0] = d[0];
			for (int i = 1; i < n; ++i) a[i] = a[i - 1] + d[i];
			return a;
		}

		// O(n)
		// d をそのまま使います。
		public long[] GetAll0()
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
		int nx, ny;
		long[,] d;
		public StaticRAQ2(int _nx, int _ny) { nx = _nx; ny = _ny; d = new long[nx, ny]; }

		// O(1)
		// 範囲外のインデックスも可。
		public void Add(int x1, int y1, int x2, int y2, long v)
		{
			if (x2 < 0 || nx <= x1) return;
			if (y2 < 0 || ny <= y1) return;
			d[Math.Max(0, x1), Math.Max(0, y1)] += v;
			if (y2 < ny) d[Math.Max(0, x1), y2] -= v;
			if (x2 < nx) d[x2, Math.Max(0, y1)] -= v;
			if (x2 < nx && y2 < ny) d[x2, y2] += v;
		}

		// O(nx ny)
		public long[,] GetAll()
		{
			var a = new long[nx, ny];
			for (int i = 0; i < nx; ++i) a[i, 0] = d[i, 0];
			for (int i = 0; i < nx; ++i)
				for (int j = 1; j < ny; ++j) a[i, j] = a[i, j - 1] + d[i, j];
			for (int j = 0; j < ny; ++j)
				for (int i = 1; i < nx; ++i) a[i, j] += a[i - 1, j];
			return a;
		}

		// O(nx ny)
		// d をそのまま使います。
		public long[,] GetAll0()
		{
			for (int i = 0; i < nx; ++i)
				for (int j = 1; j < ny; ++j) d[i, j] += d[i, j - 1];
			for (int j = 0; j < ny; ++j)
				for (int i = 1; i < nx; ++i) d[i, j] += d[i - 1, j];
			return d;
		}
	}
}
