using System;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/atc001/tasks/fft_c
	// Test: https://atcoder.jp/contests/practice2/tasks/practice2_f
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/6/NTL/2/NTL_2_F
	public class Ntt
	{
		//const long p = 469762049, g = 3;
		//const long p = 754974721, g = 11;
		//const long p = 1004535809, g = 3;
		const long p = 998244353, g = 3;

		public static int ToPowerOf2(int length)
		{
			var n = 1;
			while (n < length) n <<= 1;
			return n;
		}

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

		int n;
		long nInv;
		long[] roots;
		public Ntt(int length)
		{
			n = ToPowerOf2(length);
			nInv = MPow(n, p - 2);
			roots = NthRoots(n);
		}

		void FftInternal(long[] c, bool inverse)
		{
			var m = c.Length;
			if (m == 1) return;

			var m2 = m / 2;
			var nm = n / m;
			var c1 = new long[m2];
			var c2 = new long[m2];
			for (int i = 0; i < m2; ++i)
			{
				c1[i] = c[2 * i];
				c2[i] = c[2 * i + 1];
			}

			FftInternal(c1, inverse);
			FftInternal(c2, inverse);

			for (int i = 0; i < m2; ++i)
			{
				var z = c2[i] * roots[nm * (inverse ? m - i : i)] % p;
				c[i] = (c1[i] + z) % p;
				c[m2 + i] = (c1[i] - z + p) % p;
			}
		}

		// { f(w^i) }
		// 長さは n 以下で OK。
		public long[] Fft(long[] c, bool inverse = false)
		{
			var r = new long[n];
			c.CopyTo(r, 0);
			FftInternal(r, inverse);
			if (inverse) for (int i = 0; i < n; ++i) r[i] = r[i] * nInv % p;
			return r;
		}

		// 長さは n 以下で OK。
		public static long[] Convolution(long[] a, long[] b)
		{
			var ntt = new Ntt(a.Length + b.Length - 1);
			var fa = ntt.Fft(a);
			var fb = ntt.Fft(b);
			for (int i = 0; i < ntt.n; ++i) fa[i] = fa[i] * fb[i] % p;
			return ntt.Fft(fa, true);
		}
	}
}
