using System;

namespace CoderLib6.Collections
{
	// Test: https://judge.yosupo.jp/problem/static_range_sum
	class CumSum
	{
		long[] s;
		public CumSum(int[] a)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}
		public long Sum(int l_in, int r_ex) => s[r_ex] - s[l_in];
	}
	// 重めの処理です。
	//public long Sum(Range r) => Sum(r.Start.GetOffset(s.Length - 1), r.End.GetOffset(s.Length - 1));

	static class Seq
	{
		public static TR[] Cumulate<TS, TR>(this TS[] a, TR r0, Func<TR, TS, TR> func)
		{
			var r = new TR[a.Length + 1];
			r[0] = r0;
			for (int i = 0; i < a.Length; ++i) r[i + 1] = func(r[i], a[i]);
			return r;
		}
		public static int[] CumMax(this int[] a, int v0 = int.MinValue) => Cumulate(a, v0, Math.Max);
		public static int[] CumMin(this int[] a, int v0 = int.MaxValue) => Cumulate(a, v0, Math.Min);
	}
}
