using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BasicTest
{
	// Tab for indentation.

	static int Gcd(int x, int y)
	{
		int r;
		while ((r = x % y) != 0) { x = y; y = r; }
		return y;
	}

	static int Lcm(int x, int y) => x / Gcd(x, y) * y;

	// For negative integers.
	static int Mod(int v, int mod) => (v %= mod) < 0 ? v + mod : v;

	static long Pow(long b, int i)
	{
		if (i == 0) return 1;
		if (i == 1) return b;

		var t = Pow(b, i / 2);
		return t * t * Pow(b, i % 2);
	}

	#region Test

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
	public void Mod()
	{
		var mod = 6;
		for (var i = 0; i <= 3 * mod; i++)
			Assert.AreEqual(i % mod, Mod(i, mod));
		for (var i = -1; i >= -3 * mod; i--)
			Assert.AreEqual((i + 4 * mod) % mod, Mod(i, mod));
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
	#endregion
}
