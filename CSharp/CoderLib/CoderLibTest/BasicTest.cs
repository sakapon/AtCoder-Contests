using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BasicTest
{
	// Tab for indentation.

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }

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

	static long Pow2(long b, int i)
	{
		for (var r = 1L; ; b *= b)
		{
			if (i % 2 > 0) r *= b;
			if ((i /= 2) < 1) return r;
		}
	}

	// b < mod
	static int ModPow(int b, int i, int mod)
	{
		if (i == 0) return 1;
		if (i == 1) return b;
		var t = ModPow(b, i / 2, mod);
		return (int)((long)t * t % mod * ModPow(b, i % 2, mod) % mod);
	}

	// x < p
	static int ModInv(int x, int p) => ModPow(x, p - 2, p);

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

	[TestMethod]
	public void ModPow()
	{
		var p = 7;
		for (var i = 1; i < p; i++)
			Assert.AreEqual(1, ModPow(i, p - 1, p));
		p = 101;
		for (var i = 1; i < p; i++)
			Assert.AreEqual(1, ModPow(i, p - 1, p));
	}

	[TestMethod]
	public void ModInv()
	{
		var p = 7;
		for (var i = 1; i < p; i++)
		{
			var actual = ModInv(i, p);
			Assert.AreEqual(1, i * actual % p);
			Console.WriteLine($"{i}^-1 = {actual}");
		}
		p = 101;
		for (var i = 1; i < p; i++)
		{
			var actual = ModInv(i, p);
			Assert.AreEqual(1, i * actual % p);
			Console.WriteLine($"{i}^-1 = {actual}");
		}
	}
	#endregion
}
