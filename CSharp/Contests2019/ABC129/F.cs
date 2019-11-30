using System;

class F
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long l = h[0], a = h[1], b = h[2];
		M = h[3];

		var r = 0L;
		var d_max = (a + (l - 1) * b).ToString().Length;
		for (int d = a.ToString().Length; d <= d_max; d++)
		{
			var d10 = 1L;
			for (int i = 0; i < d; i++) d10 *= 10;
			var n_max = Math.Min(l - 1, (d10 - 1 - a) / b);
			var t_min = d10 / 10 - 1 - a;
			var n_min_ex = t_min < 0 ? -1 : t_min / b;

			long c = a + (n_min_ex + 1) * b, n = n_max - n_min_ex;
			var q = new long[,] { { d10 % M, 1, 0 }, { 0, 1, 1 }, { 0, 0, 1 } };
			q = MPow3(q, n);
			r = (q[0, 0] * r + q[0, 1] * (c % M) + q[0, 2] * (b % M)) % M;
		}
		Console.WriteLine(r);
	}

	static long M;

	static long[,] MPow3(long[,] b, long i)
	{
		var r = new long[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
		for (; ; b = MProduct3(b, b))
		{
			if (i % 2 > 0) r = MProduct3(r, b);
			if ((i /= 2) < 1) return r;
		}
	}

	static long[,] MProduct3(long[,] a, long[,] b)
	{
		var r = new long[3, 3];
		for (var i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
			{
				for (var k = 0; k < 3; k++) r[i, j] += a[i, k] * b[k, j];
				r[i, j] %= M;
			}
		return r;
	}
}
