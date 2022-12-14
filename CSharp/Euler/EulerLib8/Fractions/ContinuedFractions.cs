using System;
using System.Collections.Generic;
using System.Linq;
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
		public void Deconstruct(out BigInteger n, out BigInteger d) => (n, d) = (Numerator, Denominator);
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

		// 循環節の長さが 1 の場合、近似分数を列挙できます。
		public static IEnumerable<Fraction> ConvergentsForPeriod1(long a0, long a1)
		{
			BigInteger n = a1, d = 1;

			while (true)
			{
				yield return new Fraction(d + n * a0, n);
				(n, d) = (d + n * a1, n);
			}
		}

		// 連分数展開 (無理数の場合は循環型)
		public static IEnumerable<long> Expand(QuadraticIrrational x)
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
		public static IEnumerable<long> ExpandE()
		{
			yield return 2;

			for (long k = 1; ; ++k)
			{
				yield return 1;
				yield return k << 1;
				yield return 1;
			}
		}

		// x^2 - n * y^2 = 1
		public static (BigInteger x, BigInteger y) Pell(int n)
		{
			var sqrt = new QuadraticIrrational(0, n, 1);
			var cont = Expand(sqrt).ToArray();
			var (x, y) = Convergent(cont[..^1]);

			if (cont.Length % 2 == 1)
			{
				return (x, y);
			}
			else
			{
				return (x * x + n * y * y, 2 * x * y);
			}
		}
	}
}
