﻿using System;

namespace CoderLib8.Numerics
{
	static class Math2
	{
		// mod 2^64 と考えてもよい。
		public static long Pow(long b, long i)
		{
			for (long r = 1; ; b *= b)
			{
				if ((i & 1) != 0) r *= b;
				if ((i >>= 1) == 0) return r;
			}
		}

		public static long Pow2(long b, long i)
		{
			long r = 1;
			for (; i != 0; b *= b, i >>= 1) if ((i & 1) != 0) r *= b;
			return r;
		}

		[Obsolete]
		static long PowR(long b, int i)
		{
			if (i == 0) return 1;
			if (i == 1) return b;
			var t = PowR(b, i / 2);
			return t * t * PowR(b, i % 2);
		}

		// x が大きすぎると誤差が生じます。
		static bool IsSquareNumber(long x)
		{
			var r = (long)Math.Sqrt(x);
			return r * r == x;
		}

		// 66_C_33 は Int64 の範囲内です。
		static long[,] GetNcrs()
		{
			var n = 66;
			var dp = new long[n + 1, n + 1];
			for (int i = 0; i <= n; i++)
			{
				dp[i, 0] = dp[i, i] = 1;
				for (int j = 1; j < i; j++)
					dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
			}
			return dp;
		}

		// n >= 0
		public static long Factorial(int n) { for (long x = 1, i = 1; ; x *= ++i) if (i >= n) return x; }
		public static long Npr(int n, int r)
		{
			if (n < r) return 0;
			for (long x = 1, i = n - r; ; x *= ++i) if (i >= n) return x;
		}
		public static long Ncr(int n, int r) => n < r ? 0 : n - r < r ? Ncr(n, n - r) : Npr(n, r) / Factorial(r);

		static int Chmax(ref int x, int v) => x < v ? x = v : x;
		static int Chmin(ref int x, int v) => x > v ? x = v : x;
		static long Chmax(ref long x, long v) => x < v ? x = v : x;
		static long Chmin(ref long x, long v) => x > v ? x = v : x;

		// From: https://github.com/atcoder/ac-library/blob/master/atcoder/internal_math.hpp
		// Test: https://atcoder.jp/contests/practice2/tasks/practice2_c
		static long FloorSum(long n, long m, long a, long b)
		{
			var r = 0L;
			while (true)
			{
				if (a >= m)
				{
					r += n * (n - 1) / 2 * (a / m);
					a %= m;
				}
				if (b >= m)
				{
					r += n * (b / m);
					b %= m;
				}

				var yMax = a * n + b;
				if (yMax < m) return r;
				(n, m, a, b) = (yMax / m, a, m, yMax % m);
			}
		}

		[Obsolete("Use BitOperations.PopCount method.")]
		static int FlagCount(int x)
		{
			var r = 0;
			for (; x != 0; x >>= 1) if ((x & 1) != 0) ++r;
			return r;
		}
	}
}
