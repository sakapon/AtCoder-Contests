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

		static int FlagCount(int x)
		{
			var r = 0;
			for (; x != 0; x >>= 1) if ((x & 1) != 0) ++r;
			return r;
		}
	}
}