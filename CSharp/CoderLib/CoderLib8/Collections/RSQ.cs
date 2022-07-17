using System;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/5/ALDS1_5_D
// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/5/GRL_5_D
// Test: https://atcoder.jp/contests/practice2/tasks/practice2_b
// RSQ に特化したクラスです。
// 一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections
{
	public class RSQ
	{
		int n = 1;
		public int Count => n;
		long[] a;

		public RSQ(int count, long[] a0 = null)
		{
			while (n < count) n <<= 1;
			a = new long[n << 1];
			if (a0 != null)
			{
				Array.Copy(a0, 0, a, n, a0.Length);
				for (int i = n - 1; i > 0; --i) a[i] = a[i << 1] + a[(i << 1) | 1];
			}
		}

		public long this[int i]
		{
			get => a[n | i];
			set => Add(i, value - a[n | i]);
		}
		public void Add(int i, long d) { for (i |= n; i > 0; i >>= 1) a[i] += d; }

		public long this[int l, int r]
		{
			get
			{
				var s = 0L;
				for (l += n, r += n; l < r; l >>= 1, r >>= 1)
				{
					if ((l & 1) != 0) s += a[l++];
					if ((r & 1) != 0) s += a[--r];
				}
				return s;
			}
		}
	}
}
