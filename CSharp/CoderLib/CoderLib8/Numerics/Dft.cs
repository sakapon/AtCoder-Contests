using System;
using System.Numerics;

namespace CoderLib8.Numerics
{
	static class Dft
	{
		public static int[] ToInt(this Complex[] a) => Array.ConvertAll(a, c => (int)Math.Round(c.Real));
		public static long[] ToLong(this Complex[] a) => Array.ConvertAll(a, c => (long)Math.Round(c.Real));
		public static double[] ToDouble(this Complex[] a) => Array.ConvertAll(a, c => c.Real);

		static Complex[] NthRoots(int n)
		{
			var z = new Complex[n + 1];
			for (int i = 0; i <= n; ++i)
				z[i] = Complex.Exp(new Complex(0, i * 2 * Math.PI / n));
			return z;
		}

		static Complex NthRoot(int n, int i)
		{
			var t = i * 2 * Math.PI / n;
			return new Complex(Math.Cos(t), Math.Sin(t));
		}

		// n: Power of 2
		// { f(z^i) }
		public static Complex[] Fft(Complex[] c, bool inverse = false)
		{
			var n = c.Length;
			if (n == 1) return c;
			var n2 = n / 2;
			var c1 = new Complex[n2];
			var c2 = new Complex[n2];

			for (int i = 0; i < n2; ++i)
			{
				c1[i] = c[2 * i];
				c2[i] = c[2 * i + 1];
			}

			var f1 = Fft(c1, inverse);
			var f2 = Fft(c2, inverse);

			var r = new Complex[n];
			for (int i = 0; i < n2; ++i)
			{
				var z = Complex.Exp(new Complex(0, (inverse ? -i : i) * 2 * Math.PI / n));
				r[i] = f1[i] + z * f2[i];
				r[n2 + i] = f1[i] - z * f2[i];
				if (inverse)
				{
					r[i] /= 2;
					r[n2 + i] /= 2;
				}
			}
			return r;
		}

		// n: Power of 2
		static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			var fa = Fft(a);
			var fb = Fft(b);
			for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
			return Fft(fa, true);
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			var n = 1;
			while (n <= a.Length + b.Length - 2) n *= 2;

			var ac = new Complex[n];
			var bc = new Complex[n];
			for (int i = 0; i < a.Length; ++i) ac[i] = a[i];
			for (int i = 0; i < b.Length; ++i) bc[i] = b[i];

			return Array.ConvertAll(Convolution(ac, bc), c => (long)Math.Round(c.Real));
		}

		public static Complex[] Dft0(Complex[] c, bool inverse = false)
		{
			var n = c.Length;
			var r = new Complex[n];
			for (int i = 0; i < n; ++i)
			{
				for (int j = 0; j < n; ++j)
					r[i] += c[j] * Complex.Exp(new Complex(0, (inverse ? -i : i) * j * 2 * Math.PI / n));
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
