﻿using System;

namespace CoderLib6.Values
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/5
	public class MTwelvefold
	{
		//const long M = 998244353;
		const long M = 1000000007;
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);

		static long[] MFactorials(int n)
		{
			var f = new long[n + 1];
			f[0] = 1;
			for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
			return f;
		}

		// kPr, kCr を O(1) で求めるため、階乗を O(k) で求めておきます。
		int k;
		long[] f, f_;
		public MTwelvefold(int boxes)
		{
			k = boxes;
			f = MFactorials(k);
			f_ = Array.ConvertAll(f, MInv);
		}

		public long MFactorial(int n) => f[n];
		public long MInvFactorial(int n) => f_[n];
		public long MNpr(int n, int r) => f[n] * f_[n - r] % M;
		public long MNcr(int n, int r) => f[n] * f_[n - r] % M * f_[r] % M;

		// 球: 区別する、箱: 区別する、箱に対する個数は自由
		// 各球に対し、独立に k 通りを選びます。
		public long Power(int balls) => MPow(k, balls);

		// 球: 区別する、箱: 区別する、箱に1個以上
		// 包除原理により、0個になる場合を除きます。
		public long Surjection(int balls)
		{
			var r = 0L;
			for (int i = 0; i < k; ++i)
				r = MInt(r + (i % 2 == 0 ? 1 : -1) * MNcr(k, i) * MPow(k - i, balls));
			return r;
		}

		// 球: 区別する、箱: 区別しない、箱に1個以上
		public long Stirling(int balls) => f_[k] * Surjection(balls) % M;

		// 球: 区別する、箱: 区別しない、箱に対する個数は自由
		public long Bell(int balls)
		{
			var t = new long[k + 1];
			t[0] = 1;
			for (int i = 1; i <= k; ++i)
				t[i] = MInt(t[i - 1] + (i % 2 == 0 ? 1 : -1) * f_[i]);

			var r = 0L;
			for (int i = 1; i <= k; ++i)
				r = MInt(r + MPow(i, balls) * f_[i] % M * t[k - i]);
			return r;
		}

		public long[,] PartitionDP(int balls)
		{
			var n = balls;
			var dp = new long[n + 1, k + 1];
			dp[0, 0] = 1;

			for (int i = 0; i <= n; ++i)
				for (int j = 1; j <= k; ++j)
				{
					dp[i, j] = dp[i, j - 1];
					if (i >= j) dp[i, j] = (dp[i, j] + dp[i - j, j]) % M;
				}
			return dp;
		}

		// 球: 区別しない、箱: 区別しない、箱に対する個数は自由
		public long Partition(int balls) => PartitionDP(balls)[balls, k];
		// 球: 区別しない、箱: 区別しない、箱に1個以上
		public long PartitionPositive(int balls) => balls < k ? 0 : Partition(balls - k);
	}
}
