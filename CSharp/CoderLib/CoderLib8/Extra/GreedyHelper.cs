using System;

namespace CoderLib8.Extra
{
	public static class GreedyHelper
	{
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
