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

	static long[] Primes0(long m, long M)
	{
		var ps = new List<long>();
		for (var i = 2L; i <= M; i++)
		{
			var ri = (long)Math.Sqrt(i);
			if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
		}
		return ps.SkipWhile(i => i < m).ToArray();
	}

	// m >= 2
	static long[] Primes(long m, long M)
	{
		var ps = new List<long>();
		var rM = Math.Min((long)Math.Sqrt(M), m - 1);
		ri = 1L;
		for (var i = 3L; i <= rM; i += 2) AddPrime(ps, i);
		ri = (long)Math.Sqrt(m);
		for (var i = m % 2 == 1 ? m : m + 1; i <= M; i += 2) AddPrime(ps, i);
		return ps.Prepend(2).SkipWhile(i => i < m).ToArray();
	}
	static long ri;
	static void AddPrime(List<long> ps, long i)
	{
		if ((ri + 1) * (ri + 1) <= i) ri++;
		if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
	}

	static long[] Factorize(long v)
	{
		var ps = Primes(2, v / 2);
		var r = new List<long>();
		foreach (var p in ps)
		{
			while (v % p == 0)
			{
				r.Add(p);
				v /= p;
			}
			if (v == 1) return r.ToArray();
		}
		throw new InvalidOperationException();
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
	public void Primes()
	{
		Console.WriteLine(string.Join(" ", Primes(2, 100)));
		Console.WriteLine(string.Join(" ", Primes(999999900, 1000000100)));
		Console.WriteLine(string.Join(" ", Primes(999999999900, 1000000000100)));
	}

	[TestMethod]
	public void Factorize()
	{
		CollectionAssert.AreEqual(new long[] { 2, 31 }, Factorize(62));
		CollectionAssert.AreEqual(new long[] { 2, 2, 2, 3, 5 }, Factorize(120));
		CollectionAssert.AreEqual(new long[] { 2, 3, 3, 3, 37 }, Factorize(1998));
		CollectionAssert.AreEqual(new long[] { 3, 23, 29 }, Factorize(2001));
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
