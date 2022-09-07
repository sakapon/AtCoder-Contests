using System;

// dynamic range add
// セグメント木による実装です。
// RAQ に特化した実装であり、一般的なセグメント木のアルゴリズムとは異なります。
namespace CoderLib8.Collections.Dynamics.Int
{
	public class IntSegmentRangeAdd
	{
		readonly int n = 1;
		readonly long[] c;

		public IntSegmentRangeAdd(int itemsCount, long[] counts = null)
		{
			while (n < itemsCount) n <<= 1;
			c = new long[n << 1];
			if (counts != null) Array.Copy(counts, 0, c, n, counts.Length);
		}

		public int ItemsCount => n;

		public long this[int i]
		{
			get
			{
				var s = 0L;
				for (i |= n; i > 0; i >>= 1) s += c[i];
				return s;
			}
			set => c[n | i] += value - this[i];
		}

		public void Add(int l, int r, long d = 1)
		{
			for (l += n, r += n; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) c[l++] += d;
				if ((r & 1) != 0) c[--r] += d;
			}
		}
	}
}
