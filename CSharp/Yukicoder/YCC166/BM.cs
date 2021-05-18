using System;

class BM
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		ModuloMatrixHelper.M = m;

		var a = new long[2, 2];
		a[0, 0] = a[0, 1] = a[1, 0] = 1;

		a = ModuloMatrixHelper.MPow(a, n - 1);
		return a[1, 0];
	}
}

static class ModuloMatrixHelper
{
	public static long M = 1000000007;

	public static long[,] Unit(int n)
	{
		var r = new long[n, n];
		for (int i = 0; i < n; i++) r[i, i] = 1;
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
		for (var i = 0; i < n; i++)
			for (var j = 0; j < n; j++)
				for (var k = 0; k < n; k++)
					r[i, j] = (r[i, j] + a[i, k] * b[k, j]) % M;
		return r;
	}

	public static long[] MMul(long[,] a, long[] v)
	{
		var n = v.Length;
		var r = new long[n];
		for (var i = 0; i < n; i++)
			for (var k = 0; k < n; k++)
				r[i] = (r[i] + a[i, k] * v[k]) % M;
		return r;
	}
}
