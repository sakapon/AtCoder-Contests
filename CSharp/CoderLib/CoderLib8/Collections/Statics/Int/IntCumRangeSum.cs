using System;

// static range sum
// 累積和による実装です。
namespace CoderLib8.Collections.Statics.Int
{
	public class IntCumRangeSum
	{
		readonly int n;
		readonly long[] s;

		public IntCumRangeSum(long[] counts)
		{
			n = counts.Length;
			s = new long[n + 1];
			for (int i = 0; i < n; ++i) s[i + 1] = s[i] + counts[i];
		}

		public int ItemsCount => n;
		public long Sum => s[n];
		public long[] CumSum => s;

		public long this[int i] => s[i + 1] - s[i];
		public long this[int l, int r] => s[r] - s[l];
	}
}
