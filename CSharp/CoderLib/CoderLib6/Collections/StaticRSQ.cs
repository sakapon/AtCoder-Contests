using System;

namespace CoderLib6.Collections
{
	// Test: https://judge.yosupo.jp/problem/static_range_sum
	public class StaticRSQ1
	{
		long[] s;
		public StaticRSQ1(int[] a)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}
		public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
	}
	// 重めの処理です。
	//public long Sum(Range r) => Sum(r.Start.GetOffset(s.Length - 1), r.End.GetOffset(s.Length - 1));
}
