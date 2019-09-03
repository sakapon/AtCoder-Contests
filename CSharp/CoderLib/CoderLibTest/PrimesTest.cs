using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PrimesTest
{
	static int[] Primes0(int m, int M) => Primes0(M).SkipWhile(i => i < m).ToArray();
	static List<int> Primes0(int M)
	{
		var ps = new List<int>();
		for (int i = 2, ri = 1; i <= M; ri = (int)Math.Sqrt(++i)) if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
		return ps;
	}

	// m >= 2
	static long[] Primes(long m, long M)
	{
		var ps = new List<long>();
		var rM = Math.Min((long)Math.Sqrt(M), m - 1);
		for (var i = 2L; i <= rM; AddPrime(ps, i++)) ;
		for (var i = m; i <= M; AddPrime(ps, i++)) ;
		return ps.SkipWhile(i => i < m).ToArray();
	}
	static void AddPrime(List<long> ps, long i)
	{
		var ri = (long)Math.Sqrt(i);
		if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
	}

	// m >= 2
	static long[] Primes2(long m, long M)
	{
		var ps = new List<long>();
		var rM = Math.Min((long)Math.Sqrt(M), m - 1);
		ri = 1L;
		for (var i = 3L; i <= rM; i += 2) AddPrime2(ps, i);
		ri = (long)Math.Sqrt(m);
		for (var i = m % 2 == 1 ? m : m + 1; i <= M; i += 2) AddPrime2(ps, i);
		return ps.Prepend(2).SkipWhile(i => i < m).ToArray();
	}
	static long ri;
	static void AddPrime2(List<long> ps, long i)
	{
		if ((ri + 1) * (ri + 1) <= i) ri++;
		if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
	}

	static long[] Factorize(long v)
	{
		var r = new List<long>();
		foreach (var p in Primes(2, v / 2))
		{
			while (v % p == 0)
			{
				r.Add(p);
				v /= p;
			}
			if (v == 1) break;
		}
		return r.ToArray();
	}

	static long[] Divisors(long v)
	{
		var d = new List<long>();
		var c = 0;
		for (long i = 1, j, rv = (long)Math.Sqrt(v); i <= rv; i++)
			if (v % i == 0)
			{
				d.Insert(c, i);
				if ((j = v / i) != i) d.Insert(++c, j);
			}
		return d.ToArray();
	}

	static long[] Divisors2(long v)
	{
		if (v == 1) return new[] { 1L };
		var ps = Factorize(v).GroupBy(p => p).Select(g => new KeyValuePair<long, int>(g.Key, g.Count())).ToArray();
		var ds = new int[ps.Length];
		var c = ps.Aggregate(1L, (x, p) => x * (p.Value + 1));

		var d = new List<long> { 1 };
		var t = 1L;
		for (var i = 1; i < c; i++) d.Add(t = NextDivisor(ps, ds, t, 0));
		return d.ToArray();
	}
	static long NextDivisor(KeyValuePair<long, int>[] ps, int[] ds, long v, int i)
	{
		if (ds[i] < ps[i].Value)
		{
			ds[i]++;
			return v * ps[i].Key;
		}
		else
		{
			ds[i] = 0;
			for (int j = 0; j < ps[i].Value; j++) v /= ps[i].Key;
			return NextDivisor(ps, ds, v, i + 1);
		}
	}

	#region Test Methods

	[TestMethod]
	public void Primes0()
	{
		Console.WriteLine(string.Join(" ", Primes0(100)));
		Assert.AreEqual(25, Primes0(100).Count);
		Assert.AreEqual(168, Primes0(1000).Count);
		Assert.AreEqual(1229, Primes0(10000).Count);
		Assert.AreEqual(9592, Primes0(100000).Count);
	}

	[TestMethod]
	public void Primes0_Large()
	{
		Console.WriteLine(string.Join(" ", Primes0(1000000, 1000100)));
	}

	[TestMethod]
	public void Primes()
	{
		Console.WriteLine(string.Join(" ", Primes(2, 100)));
		Assert.AreEqual(25, Primes(2, 100).Length);
		Assert.AreEqual(143, Primes(100, 1000).Length);
		Assert.AreEqual(1061, Primes(1000, 10000).Length);
		Assert.AreEqual(8363, Primes(10000, 100000).Length);
		Console.WriteLine(string.Join(" ", Primes(998244300, 998244400)));
		Console.WriteLine(string.Join(" ", Primes(999999900, 1000000100)));
	}

	[TestMethod]
	public void Primes_Large()
	{
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
	public void Divisors()
	{
		CollectionAssert.AreEqual(new long[] { 1 }, Divisors(1));
		CollectionAssert.AreEqual(new long[] { 1, 2 }, Divisors(2));
		CollectionAssert.AreEqual(new long[] { 1, 2, 4 }, Divisors(4));
		CollectionAssert.AreEqual(new long[] { 1, 5 }, Divisors(5));
		CollectionAssert.AreEqual(new long[] { 1, 3, 7, 21 }, Divisors(21));
		CollectionAssert.AreEqual(new long[] { 1, 2, 31, 62 }, Divisors(62));
		CollectionAssert.AreEqual(new long[] { 1, 2, 3, 4, 5, 6, 8, 10, 12, 15, 20, 24, 30, 40, 60, 120 }, Divisors(120));
		CollectionAssert.AreEqual(new long[] { 1, 3, 23, 29, 69, 87, 667, 2001 }, Divisors(2001));
	}
	#endregion
}
