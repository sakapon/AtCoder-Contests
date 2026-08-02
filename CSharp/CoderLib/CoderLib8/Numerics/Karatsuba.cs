using System;

namespace CoderLib8.Numerics
{
	public static class Karatsuba
	{
		public static long[] Convolution(long[] a, long[] b)
		{
			if (a.Length == 1) return new long[] { a[0] * b[0], 0 };
			var n = a.Length;
			var d = n / 2;

			var a0 = a[..d];
			var a1 = a[d..];
			var b0 = b[..d];
			var b1 = b[d..];

			var h0 = Convolution(a0, b0);
			var h1 = Convolution(Subtract(a0, a1), Subtract(b0, b1));
			var h2 = Convolution(a1, b1);
			var h3 = Subtract(Add(h0, h2), h1);

			var r = new long[n * 2];
			Array.Copy(h0, 0, r, 0, n);
			Array.Copy(h2, 0, r, n, n);
			for (int i = 0; i < n; ++i)
				r[i + d] += h3[i];
			return r;
		}

		static long[] Add(long[] v1, long[] v2)
		{
			if (v1.Length < v2.Length) (v1, v2) = (v2, v1);
			var r = (long[])v1.Clone();
			for (int i = 0; i < v2.Length; ++i)
				r[i] += v2[i];
			return r;
		}
		static long[] Negate(long[] v) => Array.ConvertAll(v, x => -x);
		static long[] Subtract(long[] v1, long[] v2) => Add(v1, Negate(v2));
	}
}
