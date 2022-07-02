using System;

// RSQ に特化したクラスです。
// 一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections
{
	public class RSQ
	{
		int n2 = 1;
		public int Count => n2;
		long[] a2;

		public RSQ(int n, long[] a = null)
		{
			while (n2 < n) n2 <<= 1;
			a2 = new long[n2 << 1];
			if (a != null)
			{
				Array.Copy(a, 0, a2, n2, a.Length);
				for (int i = n2 - 1; i > 0; --i) a2[i] = a2[i << 1] + a2[(i << 1) | 1];
			}
		}

		public long this[int i]
		{
			get => a2[n2 | i];
			set => Add(i, value - a2[n2 | i]);
		}
		public long this[int l, int r] => GetRange(l, r);
		public void Add(int i, long d) { for (i |= n2; i > 0; i >>= 1) a2[i] += d; }

		long GetRange(int l, int r)
		{
			l |= n2; r += n2;
			var s = 0L;

			for (int i = 0; l + (1 << i) <= r; ++i)
			{
				if ((l & (1 << i)) != 0)
				{
					s += a2[l >> i];
					l += 1 << i;
				}
			}
			for (int i = 0; l < r; ++i)
			{
				if ((r & (1 << i)) != 0)
				{
					s += a2[(r >> i) - 1];
					r -= 1 << i;
				}
			}
			return s;
		}
	}
}
