using System;
using System.Collections.Generic;

namespace CoderLib8.Extra
{
	public static class BruteForceHelper
	{
		public static long[] CreateAllSums(int[] a)
		{
			var n = a.Length;
			var r = new long[1 << n];
			for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
				for (int x = 0; x < pi; ++x)
					r[pi | x] = r[x] + a[i];
			return r;
		}

		public static long[] CreateAllSums_List(int[] a)
		{
			var r = new List<long> { 0 };
			for (int i = 0; i < a.Length; ++i)
				for (int j = r.Count - 1; j >= 0; --j)
					r.Add(r[j] + a[i]);
			return r.ToArray();
		}

		// この bit DP では、配るよりも貰うほうが速いです。
		public static long[] CreateAllSums_BitDP(int[] a)
		{
			var n = a.Length;
			var dp = new long[1 << n];
			for (int x = 1; x < 1 << n; ++x)
				for (int i = 0; i < n; ++i)
					if ((x & (1 << i)) != 0)
					{
						dp[x] = dp[x ^ (1 << i)] + a[i];
						break;
					}
			return dp;
		}
	}
}
