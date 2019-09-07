using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Modulo2Test
{
	const int M = 1000000007;
	static long MPow(long b, int i)
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
