using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	[TestClass]
	public class PrimesTest
	{
		// 素因数分解 O(√n)
		// n = 1 の場合は空の配列。
		// √n を超える素因数はたかだか 1 個であり、その次数は 1。
		// 候補 x を 2 または奇数に限定することで高速化できます。
		static long[] Factorize(long n)
		{
			var r = new List<long>();
			for (long x = 2; x * x <= n && n > 1; ++x)
				while (n % x == 0) { r.Add(x); n /= x; }
			if (n > 1) r.Add(n);
			return r.ToArray();
		}

		// すべての約数 O(√n)
		static long[] Divisors(long n)
		{
			var r = new List<long>();
			for (long x = 1; x * x <= n; ++x)
				if (n % x == 0) r.Add(x);
			var i = r.Count - 1;
			if (r[i] * r[i] == n) --i;
			for (; i >= 0; --i)
				r.Add(n / r[i]);
			return r.ToArray();
		}

		// 素数判定 O(√n)
		// 候補 x を 2 または奇数に限定することで高速化できます。
		static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x)
				if (n % x == 0) return false;
			return n > 1;
		}

		#region Test Methods

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
}
