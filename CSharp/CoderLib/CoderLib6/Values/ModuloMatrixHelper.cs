﻿using System;

namespace CoderLib6.Values
{
	// Square Matrix
	static class ModuloMatrixHelper
	{
		const long M = 1000000007;

		static long[,] Unit(int n)
		{
			var r = new long[n, n];
			for (int i = 0; i < n; i++) r[i, i] = 1;
			return r;
		}

		static long[,] MPow(long[,] b, long i)
		{
			var r = Unit(b.GetLength(0));
			for (; i != 0; b = MMul(b, b), i >>= 1) if ((i & 1) != 0) r = MMul(r, b);
			return r;
		}

		static long[,] MMul(long[,] a, long[,] b)
		{
			var n = a.GetLength(0);
			var r = new long[n, n];
			for (var i = 0; i < n; i++)
				for (var j = 0; j < n; j++)
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
