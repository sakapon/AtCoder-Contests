using System;
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

	// x < p
	static int ModInv(int x, int p) => ModPow(x, p - 2, p);

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
	#endregion
}
