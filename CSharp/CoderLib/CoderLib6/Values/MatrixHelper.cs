using System;

namespace CoderLib6.Values
{
	static class MatrixHelper
	{
		public static double[,] Mul(double[,] a, double[,] b)
		{
			var n = a.GetLength(0);
			var r = new double[n, n];
			for (var i = 0; i < n; ++i)
				for (var j = 0; j < n; ++j)
					for (var k = 0; k < n; ++k)
						r[i, j] += a[i, k] * b[k, j];
			return r;
		}

		public static double[] Mul(double[,] a, double[] v)
		{
			var n = v.Length;
			var r = new double[n];
			for (var i = 0; i < n; ++i)
				for (var k = 0; k < n; ++k)
					r[i] += a[i, k] * v[k];
			return r;
		}
	}
}
