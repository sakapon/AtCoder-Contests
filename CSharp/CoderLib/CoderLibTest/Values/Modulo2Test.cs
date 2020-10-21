using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Values
{
	[TestClass]
	public class Modulo2Test
	{
		const int M = 1000000007;

		// Matrix
		static long[,] UnitMatrix(int n)
		{
			var r = new long[n, n];
			for (int i = 0; i < n; i++) r[i, i] = 1;
			return r;
		}
		static long[,] MPow(long[,] b, long i)
		{
			for (var r = UnitMatrix(b.GetLength(0)); ; b = MMul(b, b))
			{
				if (i % 2 > 0) r = MMul(r, b);
				if ((i /= 2) < 1) return r;
			}
		}
		static long[,] MMul(long[,] a, long[,] b)
		{
			var n = a.GetLength(0);
			var r = new long[n, n];
			for (var i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
					for (var k = 0; k < n; k++)
						r[i, j] = (r[i, j] + a[i, k] * b[k, j]) % M;
			return r;
		}
		static long[] MMul(long[,] a, long[] v)
		{
			var n = v.Length;
			var r = new long[n];
			for (var i = 0; i < n; i++)
				for (var k = 0; k < n; k++)
					r[i] = (r[i] + a[i, k] * v[k]) % M;
			return r;
		}
	}
}
