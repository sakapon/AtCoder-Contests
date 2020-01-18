using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BasicTest
{
	// Tabs for indentation.

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
	static int Lcm(int x, int y) => x / Gcd(x, y) * y;

	static int[] Pow2_32(int n)
	{
		var p = new int[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; i++) p[i + 1] = p[i] * 2;
		return p;
	}

	static long[] Pow2_64(int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; i++) p[i + 1] = p[i] * 2;
		return p;
	}

	static long PowR(long b, int i)
	{
		if (i == 0) return 1;
		if (i == 1) return b;
		var t = PowR(b, i / 2);
		return t * t * PowR(b, i % 2);
	}

	static long Pow(long b, int i)
	{
		for (var r = 1L; ; b *= b)
		{
			if (i % 2 > 0) r *= b;
			if ((i /= 2) < 1) return r;
		}
	}

	// n >= 0
	static long Factorial(int n) { for (long x = 1, i = 1; ; x *= ++i) if (i >= n) return x; }
	static long Npr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x *= ++i) if (i >= n) return x;
	}
	static long Ncr(int n, int r) => n < r ? 0 : n - r < r ? Ncr(n, n - r) : Npr(n, r) / Factorial(r);

	#region Test Methods

	[TestMethod]
	public void Gcd()
	{
		Assert.AreEqual(1, Gcd(1, 1));
		Assert.AreEqual(1, Gcd(1, 2));
		Assert.AreEqual(2, Gcd(4, 6));
		Assert.AreEqual(6, Gcd(6, 6));
		Assert.AreEqual(3, Gcd(15, 21));
		Assert.AreEqual(15, Gcd(45, 105));
	}

	[TestMethod]
	public void Lcm()
	{
		Assert.AreEqual(1, Lcm(1, 1));
		Assert.AreEqual(2, Lcm(1, 2));
		Assert.AreEqual(12, Lcm(4, 6));
		Assert.AreEqual(6, Lcm(6, 6));
		Assert.AreEqual(105, Lcm(15, 21));
		Assert.AreEqual(315, Lcm(45, 105));
	}

	[TestMethod]
	public void Pow2()
	{
		var p30 = Enumerable.Range(0, 31).Select(i => 1 << i).ToArray();
		CollectionAssert.AreEqual(p30, Pow2_32(30));
		var p62 = Enumerable.Range(0, 63).Select(i => 1L << i).ToArray();
		CollectionAssert.AreEqual(p62, Pow2_64(62));
	}

	[TestMethod]
	public void Pow()
	{
		var b = 2;
		for (var i = 0; i <= 62; i++)
			Assert.AreEqual((long)Math.Pow(b, i), Pow(b, i));
		b = 3;
		for (var i = 0; i <= 30; i++)
			Assert.AreEqual((long)Math.Pow(b, i), Pow(b, i));
	}

	[TestMethod]
	public void Factorial()
	{
		Assert.AreEqual(1, Factorial(0));
		Assert.AreEqual(1, Factorial(1));
		Assert.AreEqual(2, Factorial(2));
		Assert.AreEqual(6, Factorial(3));
		Assert.AreEqual(24, Factorial(4));
		Assert.AreEqual(2432902008176640000, Factorial(20));
	}

	[TestMethod]
	public void Npr()
	{
		Assert.AreEqual(1, Npr(0, 0));
		Assert.AreEqual(1, Npr(1, 0));
		Assert.AreEqual(1, Npr(1, 1));
		Assert.AreEqual(0, Npr(1, 2));
		Assert.AreEqual(1, Npr(3, 0));
		Assert.AreEqual(3, Npr(3, 1));
		Assert.AreEqual(6, Npr(3, 2));
		Assert.AreEqual(6, Npr(3, 3));
		Assert.AreEqual(151200, Npr(10, 6));
	}

	[TestMethod]
	public void Ncr()
	{
		Assert.AreEqual(1, Ncr(0, 0));
		Assert.AreEqual(1, Ncr(1, 0));
		Assert.AreEqual(1, Ncr(1, 1));
		Assert.AreEqual(0, Ncr(1, 2));
		Assert.AreEqual(1, Ncr(3, 0));
		Assert.AreEqual(3, Ncr(3, 1));
		Assert.AreEqual(3, Ncr(3, 2));
		Assert.AreEqual(1, Ncr(3, 3));
		Assert.AreEqual(210, Ncr(10, 6));
	}
	#endregion
}
