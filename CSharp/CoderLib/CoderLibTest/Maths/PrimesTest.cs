using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	[TestClass]
	public class PrimesTest
	{
		static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
		static int Lcm(int x, int y) => x / Gcd(x, y) * y;

		static long Gcd(long x, long y) { for (long r; (r = x % y) > 0; x = y, y = r) ; return y; }
		static long Lcm(long x, long y) => x / Gcd(x, y) * y;

		// 素因数分解 O(√n)
		// n = 1 の場合は空の配列。
		// √n を超える素因数はたかだか 1 個であり、その次数は 1。
		// 候補 x を 2 または奇数に限定することで高速化できます。
		static long[] Factorize(long n)
		{
			var r = new List<long>();
			for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
			if (n > 1) r.Add(n);
			return r.ToArray();
		}

		// すべての約数 O(√n)
		static long[] Divisors(long n)
		{
			var r = new List<long>();
			for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
			var i = r.Count - 1;
			if (r[i] * r[i] == n) --i;
			for (; i >= 0; --i) r.Add(n / r[i]);
			return r.ToArray();
		}

		// 素数判定 O(√n)
		// 候補 x を 2 または奇数に限定することで高速化できます。
		static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
			return n > 1;
		}

		// n 以下の素数 O(n)?
		// 候補 x を奇数に限定することで高速化できます。
		static int[] GetPrimes(int n)
		{
			var b = new bool[n + 1];
			for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
			var r = new List<int>();
			for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
			return r.ToArray();
		}

		// 範囲内の素数 O(√M)? or O(M - m)?
		// M が大きい場合、誤差が生じる可能性があります。
		static long[] GetPrimes(long m, long M)
		{
			var rn = (int)Math.Ceiling(Math.Sqrt(M));
			var b1 = new bool[rn + 1];
			var b2 = new bool[M - m + 1];
			if (m == 1) b2[0] = true;
			for (long p = 2; p <= rn; ++p)
				if (!b1[p])
				{
					for (var x = p * p; x <= rn; x += p) b1[x] = true;
					for (var x = Math.Max(p, (m + p - 1) / p) * p; x <= M; x += p) b2[x - m] = true;
				}

			var r = new List<long>();
			for (int x = 0; x < b2.Length; ++x) if (!b2[x]) r.Add(m + x);
			return r.ToArray();
		}

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

		[TestMethod]
		public void IsPrime()
		{
			Action<long> Test = n => Assert.AreEqual(Factorize(n).Length == 1, IsPrime(n));

			for (int i = 1; i <= 100; i++) Test(i);

			Test(1000000000037); // 10^12
			Test(1000000000039);
			Test(1L << 40);
			Test((1L << 40) - 1);
		}

		[TestMethod]
		public void GetPrimes()
		{
			Console.WriteLine(string.Join(" ", GetPrimes(100)));

			Assert.AreEqual(0, GetPrimes(1).Length);
			Assert.AreEqual(1, GetPrimes(2).Length);
			Assert.AreEqual(25, GetPrimes(100).Length);
			Assert.AreEqual(168, GetPrimes(1000).Length);
			Assert.AreEqual(1229, GetPrimes(10000).Length);
			Assert.AreEqual(9592, GetPrimes(100000).Length);
			Assert.AreEqual(78498, GetPrimes(1000000).Length);
		}

		[TestMethod]
		public void GetPrimes_Range()
		{
			Console.WriteLine(string.Join(" ", GetPrimes(1, 100)));
			Assert.AreEqual(25, GetPrimes(1, 100).Length);
			Assert.AreEqual(143, GetPrimes(100, 1000).Length);
			Assert.AreEqual(1061, GetPrimes(1000, 10000).Length);
			Assert.AreEqual(8363, GetPrimes(10000, 100000).Length);
			Console.WriteLine(string.Join(" ", GetPrimes(998244300, 998244400)));
			Console.WriteLine(string.Join(" ", GetPrimes(999999900, 1000000100)));
			Console.WriteLine(string.Join(" ", GetPrimes(999999999900, 1000000000100)));
			Console.WriteLine(GetPrimes(1000000000000, 1000001000000).Length);
		}
		#endregion
	}
}
