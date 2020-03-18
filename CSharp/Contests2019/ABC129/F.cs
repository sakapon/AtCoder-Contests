using System;

class F
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long l = h[0], a = h[1], b = h[2];
		M = h[3];

		long r = 0, n_min = 0;
		var d_max = (a + (l - 1) * b).ToString().Length;
		for (int d = a.ToString().Length; d <= d_max; d++)
		{
			var d10 = 1L;
			for (int i = 0; i < d; i++) d10 *= 10;
			var n_max = Math.Min(l - 1, (d10 - 1 - a) / b);

			long c = a + n_min * b, n = n_max - n_min + 1;
			n_min = ++n_max;

			var q = new[,] { { d10 % M, 1, 0 }, { 0, 1, 1 }, { 0, 0, 1 } };
			var qn = MPow(q, n);
			r = MMul(qn, new[] { r, c % M, b % M })[0];
		}
		Console.WriteLine(r);
	}

	static long M;

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
