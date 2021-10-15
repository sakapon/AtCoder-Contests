using System;

namespace CoderLib6.Values
{
	// Uses Square Matrices
	static class ModuloMatrixHelper
	{
		const long M = 1000000007;
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;

		public static long[,] Unit(int n)
		{
			var r = new long[n, n];
			for (var i = 0; i < n; ++i) r[i, i] = 1;
			return r;
		}

		public static long[,] MPow(long[,] b, long i)
		{
			var r = Unit(b.GetLength(0));
			for (; i != 0; b = MMul(b, b), i >>= 1) if ((i & 1) != 0) r = MMul(r, b);
			return r;
		}

		public static long[,] MMul(long[,] a, long[,] b)
		{
			var n = a.GetLength(0);
			var r = new long[n, n];
			for (var i = 0; i < n; ++i)
				for (var j = 0; j < n; ++j)
					for (var k = 0; k < n; ++k)
						r[i, j] = MInt(r[i, j] + a[i, k] * b[k, j]);
			return r;
		}

		public static long[] MMul(long[,] a, long[] v)
		{
			var n = v.Length;
			var r = new long[n];
			for (var i = 0; i < n; ++i)
				for (var k = 0; k < n; ++k)
					r[i] = MInt(r[i] + a[i, k] * v[k]);
			return r;
		}

		public static long[,] Transpose(long[,] a)
		{
			var n = a.GetLength(0);
			var m = a.GetLength(1);
			var r = new long[m, n];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j, i] = a[i, j];
			return r;
		}
	}
}
