﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerLib8.Fractions
{
	[System.Diagnostics.DebuggerDisplay(@"{Numerator} / {Denominator}")]
	public struct Fraction
	{
		public BigInteger Numerator, Denominator;

		public Fraction(BigInteger numerator, BigInteger denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}
	}

	// 二次無理数 (a + √c) / d
	[System.Diagnostics.DebuggerDisplay(@"{Value}")]
	public struct QuadraticIrrational
	{
		public long a, c, d;
		public double Value => (a + Math.Sqrt(c)) / d;

		public QuadraticIrrational(long a, long c, long d)
		{
			this.a = a;
			this.c = c;
			this.d = d;
		}

		// d * (a - √c) / (a * a - c)
		public QuadraticIrrational Inverse
		{
			get
			{
				// 必ず d で割り切れる？
				return new QuadraticIrrational(-a, c, (c - a * a) / d);
			}
		}
	}

	public static class ContinuedFractions
	{
		// 連分数展開 (循環するとは限らない) から近似分数を求めます。
		public static Fraction Convergent(long[] a)
		{
			BigInteger n = a[^1], d = 1;

			for (int i = a.Length - 2; i >= 0; --i)
			{
				(n, d) = (d + n * a[i], n);
			}
			return new Fraction(n, d);
		}

		// 連分数展開 (無理数の場合は循環型)
		public static IEnumerable<long> Continued(QuadraticIrrational x)
		{
			var set = new HashSet<QuadraticIrrational>();
			long i;

			while (true)
			{
				(i, x) = Next(x);
				yield return i;
				if (!set.Add(x)) break;
			}
		}

		// (整数部分, 小数部分の逆数)
		static (long, QuadraticIrrational) Next(QuadraticIrrational x)
		{
			var i = (long)Math.Floor(x.Value);
			return (i, new QuadraticIrrational(x.a - i * x.d, x.c, x.d).Inverse);
		}

		// e の連分数展開
		public static IEnumerable<long> ContinuedE()
		{
			yield return 2;

			for (long k = 1; ; ++k)
			{
				yield return 1;
				yield return k << 1;
				yield return 1;
			}
		}
	}
}
