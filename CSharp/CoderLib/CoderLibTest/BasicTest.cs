using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BasicTest
{
	// Tabs for indentation.

	static int[] CumSum(int[] a)
	{
		var s = new int[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
	static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static int[] Pow2() => Enumerable.Range(0, 31).Select(i => 1 << i).ToArray();
	static long[] Pow2L() => Enumerable.Range(0, 63).Select(i => 1L << i).ToArray();

	static int[] Powers(int n, int b)
	{
		var p = new int[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
	static long[] Powers(int n, long b)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
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
	public void CumSum()
	{
		Assert.AreEqual(500500, CumSum(Enumerable.Range(1, 1000).ToArray())[1000]);
		Assert.AreEqual(5000050000, CumSumL(Enumerable.Range(1, 100000).ToArray())[100000]);
	}

	[TestMethod]
	public void Powers()
	{
		CollectionAssert.AreEqual(Pow2(), Powers(30, 2));
		CollectionAssert.AreEqual(Pow2L(), Powers(62, 2L));
		Assert.AreEqual(4052555153018976267, Powers(39, 3L)[39]);
	}

	[TestMethod]
	public void Pow()
	{
		var b = 2;
		for (var i = 0; i <= 62; i++)
			Assert.AreEqual((long)Math.Pow(b, i), Math2.Pow(b, i));
		b = 3;
		for (var i = 0; i <= 33; i++)
			Assert.AreEqual((long)Math.Pow(b, i), Math2.Pow(b, i));
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
