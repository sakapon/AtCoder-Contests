using System;

// dynamic range sum
// セグメント木による実装です。
// RSQ に特化した実装であり、一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections.Dynamics.Int
{
	public class IntSegmentRangeSum
	{
		readonly int n = 1;
		readonly long[] c;

		public IntSegmentRangeSum(int itemsCount, long[] counts = null)
		{
			while (n < itemsCount) n <<= 1;
			c = new long[n << 1];
			if (counts != null)
			{
				Array.Copy(counts, 0, c, n, counts.Length);
				for (int i = n - 1; i > 0; --i) c[i] = c[i << 1] + c[(i << 1) | 1];
			}
		}

		public int ItemsCount => n;
		public long Sum => c[1];

		public long this[int i]
		{
			get => c[n | i];
			set => Add(i, value - c[n | i]);
		}

		public long this[int l, int r]
		{
			get
			{
				var s = 0L;
				for (l += n, r += n; l < r; l >>= 1, r >>= 1)
				{
					if ((l & 1) != 0) s += c[l++];
					if ((r & 1) != 0) s += c[--r];
				}
				return s;
			}
		}

		public void Add(int i, long d = 1) { for (i |= n; i > 0; i >>= 1) c[i] += d; }
	}
}
