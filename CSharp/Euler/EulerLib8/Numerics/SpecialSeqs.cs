using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerLib8.Numerics
{
	public static class SpecialSeqs
	{
		public static IEnumerable<BigInteger> FibonacciSeq(BigInteger f0, BigInteger f1)
		{
			yield return f0;
			yield return f1;

			while (true)
			{
				(f0, f1) = (f1, f0 + f1);
				yield return f1;
			}
		}

		public static IEnumerable<(long a, long b, long c)> PythagoreanTriples(long mMax)
		{
			for (long m = 2; m <= mMax; ++m)
			{
				var m2 = m * m;
				for (long n = 1; n < m; ++n)
				{
					if ((m & 1) == (n & 1)) continue;
					if (Primes.Gcd(m, n) != 1) continue;

					var n2 = n * n;
					var a = 2 * m * n;
					var b = m2 - n2;
					var c = m2 + n2;
					if (a > b) (a, b) = (b, a);
					yield return (a, b, c);
				}
			}
		}

		// n 角数
		public static long PolygonalNumber(long n, long i)
		{
			return i * ((n - 2) * i + 4 - n) / 2;
		}

		// n 角数
		public static IEnumerable<long> PolygonalNumbers(long n)
		{
			n -= 2;
			for (var (v, d) = (0L, 1L); ; v += d, d += n) yield return v;
		}
	}
}
