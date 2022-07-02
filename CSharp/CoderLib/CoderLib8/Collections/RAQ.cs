using System;

// RAQ に特化したクラスです。
// 一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections
{
	public class RAQ
	{
		int n2 = 1;
		public int Count => n2;
		long[] a2;

		public RAQ(int n, long[] a = null)
		{
			while (n2 < n) n2 <<= 1;
			a2 = new long[n2 << 1];
			if (a != null) Array.Copy(a, 0, a2, n2, a.Length);
		}

		public long this[int i]
		{
			get
			{
				var s = 0L;
				for (i |= n2; i > 0; i >>= 1) s += a2[i];
				return s;
			}
			set => a2[n2 | i] += value - this[i];
		}

		public void AddRange(int l, int r, long d)
		{
			for (l |= n2, r += n2; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) a2[l++] += d;
				if ((r & 1) != 0) a2[--r] += d;
			}
		}
	}
}
