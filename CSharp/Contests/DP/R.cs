using System;
using System.Linq;

class R
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		long n = h[0], k = h[1];

		var a = new long[n, n];
		for (int i = 0; i < n; i++)
		{
			var r = read();
			for (int j = 0; j < n; j++) a[i, j] = r[j];
		}

		var v0 = Enumerable.Repeat(1L, (int)n).ToArray();
		var vk = MMul(MPow(a, k), v0);
		Console.WriteLine(vk.Sum() % M);
	}

	static int M = 1000000007;

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
