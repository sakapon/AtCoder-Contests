using System;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/atc001/tasks/fft_c
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_f
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/6/NTL/2/NTL_2_F
	static class Ntt0
	{
		const long p = 998244353, g = 3;

		public static int ToPowerOf2(int length)
		{
			var n = 1;
			while (n < length) n <<= 1;
			return n;
		}

		static long MInt(long x, long M) => (x %= M) < 0 ? x + M : x;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
			return r;
		}

		static long[] NthRoots(int n)
		{
			var w = MPow(g, (p - 1) / n);
			var r = new long[n + 1];
			r[0] = 1;
			for (int i = 0; i < n; ++i) r[i + 1] = r[i] * w % p;
			return r;
		}

		static long NthRoot(int n, int i)
		{
			return MPow(g, (p - 1) / n * MInt(i, n));
		}

		static void FftInternal(long[] c, bool inverse)
		{
			var n = c.Length;
			if (n == 1) return;

			var n2 = n / 2;
			var c1 = new long[n2];
			var c2 = new long[n2];
			for (int i = 0; i < n2; ++i)
			{
				c1[i] = c[2 * i];
				c2[i] = c[2 * i + 1];
			}

			FftInternal(c1, inverse);
			FftInternal(c2, inverse);

			for (int i = 0; i < n2; ++i)
			{
				var z = c2[i] * NthRoot(n, inverse ? -i : i) % p;
				c[i] = (c1[i] + z) % p;
				c[n2 + i] = (c1[i] - z + p) % p;
			}
		}

		// { f(w^i) }
		// n: Power of 2
		public static long[] Fft(long[] c, bool inverse = false)
		{
			var n = c.Length;
			var nInv = MPow(n, p - 2);

			var r = new long[n];
			c.CopyTo(r, 0);
			FftInternal(r, inverse);
			if (inverse) for (int i = 0; i < n; ++i) r[i] = r[i] * nInv % p;
			return r;
		}

		// n: Power of 2
		static long[] ConvolutionInternal(long[] a, long[] b)
		{
			var fa = Fft(a);
			var fb = Fft(b);
			for (int i = 0; i < a.Length; ++i) fa[i] = fa[i] * fb[i] % p;
			return Fft(fa, true);
		}

		// 長さは n 以下で OK。
		public static long[] Convolution(long[] a, long[] b)
		{
			var n = ToPowerOf2(a.Length + b.Length - 1);
			var ac = new long[n];
			var bc = new long[n];
			a.CopyTo(ac, 0);
			b.CopyTo(bc, 0);
			return ConvolutionInternal(ac, bc);
		}

		// { f(w^i) }
		// n: Power of 2
		public static long[] Naive(long[] c, bool inverse = false)
		{
			var n = c.Length;
			var nInv = MPow(n, p - 2);
			var roots = NthRoots(n);

			var r = new long[n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
					r[i] = (r[i] + c[j] * roots[MInt((inverse ? -i : i) * j, n)]) % p;
				if (inverse) r[i] = r[i] * nInv % p;
			}
			return r;
		}

		#region Constants Search

		static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
			return n > 1;
		}

		public static (long p, long k) FindMinPK(long n)
		{
			for (long k = 1; ; k++)
			{
				var p = k * n + 1;
				if (IsPrime(p)) return (p, k);
			}
		}

		public static void FindPKs(long n, long maxK)
		{
			for (long k = 1; k <= maxK; k++)
			{
				var p = k * n + 1;
				if (IsPrime(p)) Console.WriteLine((p, k));
			}
		}

		public static long FindMinGenerator(long p)
		{
			for (long g = 1; g < p; g++)
			{
				var t = 1L;
				var count = 1;
				while ((t = t * g % p) != 1) count++;
				if (count == p - 1) return g;
			}
			throw new InvalidOperationException();
		}
		#endregion
	}
}
