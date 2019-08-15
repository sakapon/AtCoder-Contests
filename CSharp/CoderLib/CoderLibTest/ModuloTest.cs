using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ModuloTest
{
	// For negative integers.
	static int Mod(int v, int mod) => (v %= mod) < 0 ? v + mod : v;

	// b < mod
	static int ModPow(int b, int i, int mod)
	{
		if (i == 0) return 1;
		if (i == 1) return b;
		var t = ModPow(b, i / 2, mod);
		return (int)((long)t * t % mod * ModPow(b, i % 2, mod) % mod);
	}

	// b < mod
	static int ModPow2(int b, int i, int mod)
	{
		for (var r = 1; ; b = (int)((long)b * b % mod))
		{
			if (i % 2 > 0) r = (int)((long)r * b % mod);
			if ((i /= 2) < 1) return r;
		}
	}

	// x < p
	static int ModInv(int x, int p) => ModPow(x, p - 2, p);

	static int ModFactorial(int n, int mod) => Enumerable.Range(1, n).Aggregate((x, y) => x * y % mod);

	// nPr
	static int ModNpr(int n, int r, int mod) => Enumerable.Range(n - r + 1, r).Aggregate((x, y) => x * y % mod);

	// nCr
	static int ModNcr(int n, int r, int p) => ModNpr(n, r, p) * ModInv(ModFactorial(r, p), p) % p;

	#region Test Methods

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

	[TestMethod]
	public void ModFactorial()
	{
		Assert.AreEqual(3628800 % 13, ModFactorial(10, 13));
		Assert.AreEqual(3628800 % 23, ModFactorial(10, 23));
		Assert.AreEqual(3628800 % 31, ModFactorial(10, 31));
		Assert.AreEqual(3628800 % 32, ModFactorial(10, 32));
	}

	[TestMethod]
	public void ModNpr()
	{
		Assert.AreEqual(151200 % 13, ModNpr(10, 6, 13));
		Assert.AreEqual(151200 % 23, ModNpr(10, 6, 23));
		Assert.AreEqual(151200 % 31, ModNpr(10, 6, 31));
		Assert.AreEqual(151200 % 32, ModNpr(10, 6, 32));
	}

	[TestMethod]
	public void ModNcr()
	{
		Assert.AreEqual(210 % 13, ModNcr(10, 6, 13));
		Assert.AreEqual(210 % 23, ModNcr(10, 6, 23));
		Assert.AreEqual(210 % 31, ModNcr(10, 6, 31));
		// 素数でないと利用できません。
		//Assert.AreEqual(210 % 32, ModNcr(10, 6, 32));
	}
	#endregion
}
