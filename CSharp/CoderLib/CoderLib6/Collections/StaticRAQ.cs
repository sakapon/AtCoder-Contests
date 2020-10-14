using System;

namespace CoderLib6.Collections
{
	// いもす法
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/5/DSL_5_A
	class StaticRAQ
	{
		long[] d;
		public StaticRAQ(int n) { d = new long[n]; }

		// O(1)
		// 範囲外のインデックスも可。
		public void Add(int l_in, int r_ex, long v)
		{
			d[Math.Max(0, l_in)] += v;
			if (r_ex < d.Length) d[r_ex] -= v;
		}

		// O(n)
		public long[] GetAll()
		{
			var a = new long[d.Length];
			a[0] = d[0];
			for (int i = 1; i < d.Length; ++i) a[i] = a[i - 1] + d[i];
			return a;
		}
	}

	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/5/DSL_5_B
	class StaticRAQ2
	{
		int nx, ny;
		long[,] d;
		public StaticRAQ2(int _nx, int _ny) { nx = _nx; ny = _ny; d = new long[nx, ny]; }

		// O(1)
		// 範囲外のインデックスも可。
		public void Add(int x1, int y1, int x2, int y2, long v)
		{
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
	}
}
