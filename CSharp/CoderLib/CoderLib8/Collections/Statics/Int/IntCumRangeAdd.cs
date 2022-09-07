using System;

// static range add
// 累積和による実装です。
namespace CoderLib8.Collections.Statics.Int
{
	public class IntCumRangeAdd
	{
		readonly int n;
		readonly long[] d;

		public IntCumRangeAdd(int itemsCount)
		{
			n = itemsCount;
			d = new long[n];
		}

		public int ItemsCount => n;
		public long[] Delta => d;

		public void Add(int l, int r, long v = 1)
		{
			if (l < n) d[l] += v;
			if (r < n) d[r] -= v;
		}

		public long[] ToArray()
		{
			var a = new long[n];
			a[0] = d[0];
			for (int i = 1; i < n; ++i) a[i] = a[i - 1] + d[i];
			return a;
		}

		// d をそのまま使います。
		//public long[] GetArray()
		//{
		//	for (int i = 1; i < n; ++i) d[i] += d[i - 1];
		//	return d;
		//}
	}
}
