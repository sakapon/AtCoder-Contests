using System;

namespace CoderLib6.Values
{
	// 小数
	public static class MatrixHelper
	{
		public static double[,] Mul(double[,] a, double[,] b)
		{
			if (a.GetLength(1) != b.GetLength(0)) throw new InvalidOperationException();
			var n = a.GetLength(0);
			var m = b.GetLength(1);
			var l = a.GetLength(1);
			var r = new double[n, m];
			for (var i = 0; i < n; ++i)
				for (var j = 0; j < m; ++j)
					for (var k = 0; k < l; ++k)
						r[i, j] += a[i, k] * b[k, j];
			return r;
		}

		public static double[] Mul(double[,] a, double[] v)
		{
			if (a.GetLength(1) != v.Length) throw new InvalidOperationException();
			var n = a.GetLength(0);
			var l = v.Length;
			var r = new double[n];
			for (var i = 0; i < n; ++i)
				for (var k = 0; k < l; ++k)
					r[i] += a[i, k] * v[k];
			return r;
		}

		public static double[,] Transpose(double[,] a)
		{
			var n = a.GetLength(0);
			var m = a.GetLength(1);
			var r = new double[m, n];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j, i] = a[i, j];
			return r;
		}
	}
}
