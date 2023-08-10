using System;

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

		// 階乗も含まれます。
		// 20_P_20 = 20! は Int64 の範囲内です。
		// n を大きい値に変更できます。O(n^2)
		public static long[,] GetNprs()
		{
			var n = 20;
			var dp = new long[n + 1, n + 1];
			for (int i = 0; i <= n; i++)
			{
				dp[i, 0] = 1;
				for (int j = 0; j < i; j++)
					dp[i, j + 1] = dp[i, j] * (i - j);
			}
			return dp;
		}

		// 66_C_33 は Int64 の範囲内です。
		// n を大きい値に変更できます。O(n^2)
		public static long[,] GetNcrs()
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

		public static int Chmax(ref int x, int v) => x < v ? x = v : x;
		public static int Chmin(ref int x, int v) => x > v ? x = v : x;
		public static long Chmax(ref long x, long v) => x < v ? x = v : x;
		public static long Chmin(ref long x, long v) => x > v ? x = v : x;

		public static int Max(int x, int y) => x < y ? y : x;
		public static int Min(int x, int y) => x > y ? y : x;
		public static long Max(long x, long y) => x < y ? y : x;
		public static long Min(long x, long y) => x > y ? y : x;

		public static int Max(int x, int y, int z) => x < (y = y < z ? z : y) ? y : x;
		public static int Min(int x, int y, int z) => x > (y = y > z ? z : y) ? y : x;
		public static long Max(long x, long y, long z) => x < (y = y < z ? z : y) ? y : x;
		public static long Min(long x, long y, long z) => x > (y = y > z ? z : y) ? y : x;

		public static int Max(params int[] a)
		{
			var r = int.MinValue;
			foreach (var v in a) if (r < v) r = v;
			return r;
		}
		public static int Min(params int[] a)
		{
			var r = int.MaxValue;
			foreach (var v in a) if (r > v) r = v;
			return r;
		}
		public static long Max(params long[] a)
		{
			var r = long.MinValue;
			foreach (var v in a) if (r < v) r = v;
			return r;
		}
		public static long Min(params long[] a)
		{
			var r = long.MaxValue;
			foreach (var v in a) if (r > v) r = v;
			return r;
		}

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

		// 最下位・最上位ビット
		static int MinBit(int x) => -x & x;
		static int MaxBit(int x)
		{
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x ^ (x >> 1);
		}

		const ulong m1 = 0x5555555555555555;
		const ulong m2 = 0x3333333333333333;
		const ulong m4 = 0x0F0F0F0F0F0F0F0F;
		const ulong m8 = 0x00FF00FF00FF00FF;
		const ulong m16 = 0x0000FFFF0000FFFF;
		const ulong m32 = 0x00000000FFFFFFFF;

		public static int PopCount(ulong x)
		{
			x = (x & m1) + ((x >> 1) & m1);
			x = (x & m2) + ((x >> 2) & m2);
			x = (x & m4) + ((x >> 4) & m4);
			x = (x & m8) + ((x >> 8) & m8);
			x = (x & m16) + ((x >> 16) & m16);
			x = (x & m32) + ((x >> 32) & m32);
			return (int)x;
		}

		[Obsolete("Use BitOperations.PopCount method.")]
		static int FlagCount(int x)
		{
			var r = 0;
			for (; x != 0; x >>= 1) if ((x & 1) != 0) ++r;
			return r;
		}

		public static int[] PopCounts(int n)
		{
			var r = new int[1 << n];
			for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
				for (int x = 0; x < pi; ++x)
					r[x | pi] = r[x] + 1;
			return r;
		}
	}
}
