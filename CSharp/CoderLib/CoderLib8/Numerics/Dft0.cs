using System;
using System.Numerics;

namespace CoderLib8.Numerics
{
	// Test: https://atcoder.jp/contests/atc001/tasks/fft_c
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/6/NTL/2/NTL_2_F
	static class Dft0
	{
		public static int[] ToInt(this Complex[] a) => Array.ConvertAll(a, c => (int)Math.Round(c.Real));
		public static long[] ToLong(this Complex[] a) => Array.ConvertAll(a, c => (long)Math.Round(c.Real));
		public static double[] ToDouble(this Complex[] a) => Array.ConvertAll(a, c => c.Real);

		public static int ToPowerOf2(int length)
		{
			var n = 1;
			while (n < length) n <<= 1;
			return n;
		}

		static Complex[] NthRoots(int n)
		{
			var r = new Complex[n + 1];
			for (int i = 0; i <= n; ++i) r[i] = Complex.Exp(new Complex(0, i * 2 * Math.PI / n));
			return r;
		}

		static Complex NthRoot(int n, int i)
		{
			var t = i * 2 * Math.PI / n;
			return new Complex(Math.Cos(t), Math.Sin(t));
		}

		static void FftInternal(Complex[] c, bool inverse)
		{
			var n = c.Length;
			if (n == 1) return;

			var n2 = n / 2;
			var c1 = new Complex[n2];
			var c2 = new Complex[n2];
			for (int i = 0; i < n2; ++i)
			{
				c1[i] = c[2 * i];
				c2[i] = c[2 * i + 1];
			}

			FftInternal(c1, inverse);
			FftInternal(c2, inverse);

			for (int i = 0; i < n2; ++i)
			{
				var z = c2[i] * NthRoot(n, inverse ? -i : i);
				c[i] = c1[i] + z;
				c[n2 + i] = c1[i] - z;
			}
		}

		// { f(w^i) }
		// n: Power of 2
		public static Complex[] Fft(Complex[] c, bool inverse = false)
		{
			var n = c.Length;
			var r = new Complex[n];
			c.CopyTo(r, 0);
			FftInternal(r, inverse);
			if (inverse) for (int i = 0; i < n; ++i) r[i] /= n;
			return r;
		}

		// n: Power of 2
		static Complex[] ConvolutionInternal(Complex[] a, Complex[] b)
		{
			var fa = Fft(a);
			var fb = Fft(b);
			for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
			return Fft(fa, true);
		}

		// 長さは n 以下で OK。
		public static long[] Convolution(long[] a, long[] b)
		{
			var n = ToPowerOf2(a.Length + b.Length - 1);
			var ac = new Complex[n];
			var bc = new Complex[n];
			for (int i = 0; i < a.Length; ++i) ac[i] = a[i];
			for (int i = 0; i < b.Length; ++i) bc[i] = b[i];
			return ConvolutionInternal(ac, bc).ToLong();
		}

		public static Complex[] Naive(Complex[] c, bool inverse = false)
		{
			var n = c.Length;
			var r = new Complex[n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
					r[i] += c[j] * NthRoot(n, (inverse ? -i : i) * j);
				if (inverse) r[i] /= n;
			}
			return r;
		}

		// non-recursive
		public static Complex[] Fft2(Complex[] c, bool inverse = false)
		{
			var n = c.Length;
			var roots = NthRoots(n);
			var p = (Complex[])c.Clone();
			var t = new Complex[n];

			for (int m = 2; m <= n; m <<= 1)
			{
				var m2 = m >> 1;
				for (int i = 0; i < n; ++i)
				{
					var si = i / m; // section index
					t[i] = p[si * m2 + i % m2] + p[si * m2 + i % m2 + (n >> 1)] * roots[inverse ? n - n / m * i % n : n / m * i % n];
					if (inverse) t[i] /= 2;
				}
				(p, t) = (t, p);
			}
			return p;
		}
	}
}
