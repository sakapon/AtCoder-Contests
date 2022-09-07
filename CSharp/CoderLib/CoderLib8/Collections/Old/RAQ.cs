using System;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/3/DSL/2/DSL_2_E
// RAQ に特化したクラスです。
// 一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections
{
	public class RAQ
	{
		int n = 1;
		public int Count => n;
		long[] a;

		public RAQ(int count, long[] a0 = null)
		{
			while (n < count) n <<= 1;
			a = new long[n << 1];
			if (a0 != null) Array.Copy(a0, 0, a, n, a0.Length);
		}

		public long this[int i]
		{
			get
			{
				var s = 0L;
				for (i |= n; i > 0; i >>= 1) s += a[i];
				return s;
			}
			set => a[n | i] += value - this[i];
		}

		public void AddRange(int l, int r, long d)
		{
			for (l |= n, r += n; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) a[l++] += d;
				if ((r & 1) != 0) a[--r] += d;
			}
		}
	}
}
