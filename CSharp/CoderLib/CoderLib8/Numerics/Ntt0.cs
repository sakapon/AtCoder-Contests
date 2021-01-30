using System;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/atc001/tasks/fft_c
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_f
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/6/NTL/2/NTL_2_F
	static class Ntt0
	{
		const long p = 998244353;
		const long g = 3;
		const long half = (p + 1) / 2;

		static long MInt(long x, long M) => (x %= M) < 0 ? x + M : x;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
			return r;
		}

		static long[] NthRoots(int n)
		{
			var z = MPow(g, (p - 1) / n);
			var r = new long[n + 1];
			r[0] = 1;
			for (int i = 0; i < n; ++i)
				r[i + 1] = r[i] * z % p;
			return r;
		}

		static long NthRoot(int n, int i)
		{
			return MPow(g, (p - 1) / n * MInt(i, n));
		}

		// n: Power of 2
		// { f(z^i) }
		public static long[] Fft(long[] c, bool inverse = false)
		{
			var n = c.Length;
			if (n == 1) return c;

			var n2 = n / 2;
			var c1 = new long[n2];
			var c2 = new long[n2];
			for (int i = 0; i < n2; ++i)
			{
				c1[i] = c[2 * i];
				c2[i] = c[2 * i + 1];
			}

			var f1 = Fft(c1, inverse);
			var f2 = Fft(c2, inverse);

			var r = new long[n];
			for (int i = 0; i < n2; ++i)
			{
				var z = f2[i] * NthRoot(n, inverse ? -i : i) % p;
				r[i] = (f1[i] + z) % p;
				r[n2 + i] = (f1[i] - z + p) % p;
				if (inverse)
				{
					r[i] = r[i] * half % p;
					r[n2 + i] = r[n2 + i] * half % p;
				}
			}
			return r;
		}

		// n: Power of 2
		static long[] Convolution_In(long[] a, long[] b)
		{
			var fa = Fft(a);
			var fb = Fft(b);
			for (int i = 0; i < a.Length; ++i) fa[i] = fa[i] * fb[i] % p;
			return Fft(fa, true);
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			var n = 1;
			while (n <= a.Length + b.Length - 2) n *= 2;

			var ac = new long[n];
			var bc = new long[n];
			a.CopyTo(ac, 0);
			b.CopyTo(bc, 0);

			return Convolution_In(ac, bc);
		}

		public static long[] Naive(long[] c, bool inverse = false)
		{
			var n = c.Length;
			var roots = NthRoots(n);

			var n_ = 1L;
			var tn = n;
			while (tn > 1)
			{
				n_ = n_ * half % p;
				tn /= 2;
			}

			var r = new long[n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
					r[i] = (r[i] + c[j] * roots[MInt(inverse ? -i * j : i * j, n)]) % p;
				if (inverse) r[i] = r[i] * n_ % p;
			}
			return r;
		}
	}
}
