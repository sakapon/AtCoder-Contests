using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Modulo2Test
{
	const int M = 1000000007;
	static long MPow(long b, long i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MInv(long x) => MPow(x, M - 2);
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MAdd(long x, long y) => (x + y) % M;
	static long MSub(long x, long y) => MInt(x - y);
	static long MMul(long x, long y) => x * y % M;
	static long MDiv(long x, long y) => x * MInv(y) % M;

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

	#region Test Methods

	[TestMethod]
	public void MInt()
	{
		Assert.AreEqual(999999999, MInt(-8));
	}

	[TestMethod]
	public void MDiv()
	{
		Assert.AreEqual(312500008, MDiv(93, 16));
	}
	#endregion
}
