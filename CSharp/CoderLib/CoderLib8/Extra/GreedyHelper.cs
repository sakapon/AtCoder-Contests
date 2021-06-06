using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	public static class GreedyHelper
	{
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/4/DPL_4_B
		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ay
		// i に対して、たかだか 1 つの j が返ります。
		static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
		{
			for (int i = 0, j = 0; i < n1 && j < n2; ++i)
				for (; j < n2; ++j)
					if (predicate(i, j)) { yield return (i, j); break; }
		}

		// Test: https://atcoder.jp/contests/abc184/tasks/abc184_f
		static IEnumerable<(T1 v1, T2 v2)> TwoPointers<T1, T2>(T1[] a1, T2[] a2, Func<T1, T2, bool> predicate)
		{
			for (int i = 0, j = 0; i < a1.Length && j < a2.Length; ++i)
				for (; j < a2.Length; ++j)
					if (predicate(a1[i], a2[j])) { yield return (a1[i], a2[j]); break; }
		}

		// k 回目に true となる日を求めます。
		// k, day: 0-indexed
		// 0 <= trueDay < period
		public static long KthTrueDay(long k, long period, long[] trueDays)
		{
			var turns = Math.DivRem(k, trueDays.Length, out k);
			return turns * period + trueDays[k];
		}

		// Test: https://codeforces.com/contest/1501/problem/D
		// k 回目に false となる日を求めます。
		// k, day: 0-indexed
		// 0 <= trueDay < period
		public static long KthFalseDay(long k, long period, long[] trueDays)
		{
			var turns = Math.DivRem(k, period - trueDays.Length, out k);
			for (int i = 0; i < trueDays.Length; ++i)
				if (k + i < trueDays[i])
					return turns * period + k + i;
			return turns * period + k + trueDays.Length;
		}
	}
}
