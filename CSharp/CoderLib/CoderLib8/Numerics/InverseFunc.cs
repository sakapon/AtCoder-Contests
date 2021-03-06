﻿using System;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/arc108/tasks/arc108_a
	// Test: https://atcoder.jp/contests/arc109/tasks/arc109_b
	static class InverseFunc
	{
		// floor(f^{-1} (x))
		public static Func<long, long> Floor(long l, long r, Func<long, long> f) => y => Last(l, r, x => f(x) <= y);
		public static Func<long, long> FloorNaturalSum() => Floor(-1, 1L << 31, n => n * (n + 1) / 2);
		public static Func<long, long> FloorSqrt() => Floor(-1, 1L << 31, x => x * x);

		// floor(f^{-1} (0))
		public static long FloorEquation(long l, long r, Func<long, long> f, bool increasing) => Last(l, r, x => increasing ? f(x) <= 0 : f(x) >= 0);
		public static Func<long, long> FloorNaturalSum2() => s => FloorEquation(-1, 1L << 31, n => n * (n + 1) - 2 * s, true);
		public static Func<long, long> FloorSqrt2() => y => FloorEquation(-1, 1L << 31, x => x * x - y, true);

		public static bool IsSquareNumber(long x)
		{
			var r = FloorSqrt()(x);
			return r * r == x;
		}

		static long Last(long l, long r, Func<long, bool> f)
		{
			long m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}

		#region For Large long Integer

		// decimal < 2^96
		// floor(f^{-1} (x))
		public static Func<decimal, long> FloorDec(long l, long r, Func<decimal, decimal> f) => y => Last(l, r, x => f(x) <= y);
		public static Func<decimal, long> FloorSqrtDec() => FloorDec(-1, 1L << 45, x => x * x);

		public static bool IsSquareNumberDec(decimal x)
		{
			decimal r = FloorSqrtDec()(x);
			return r * r == x;
		}
		#endregion
	}
}
