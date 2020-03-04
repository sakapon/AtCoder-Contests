using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	[TestClass]
	public class PrimesLabTest
	{
		static IEnumerable<int> Range(int m, int M) { for (var i = m; i <= M; i++) yield return i; }
		static IEnumerable<long> Range(long m, long M) { for (var i = m; i <= M; i++) yield return i; }

		static int[] PrimesF(int m, int M)
		{
			var b = PrimeFlags(M);
			return Enumerable.Range(m, M - m + 1).Where(i => b[i]).ToArray();
		}
		static bool[] PrimeFlags(int M)
		{
			var rM = (int)Math.Sqrt(M);
			var b = new bool[M + 1]; b[2] = true;
			for (int i = 3; i <= M; i += 2) b[i] = true;
			for (int p = 3; p <= rM; p++) if (b[p]) for (var i = p * p; i <= M; i += 2 * p) b[i] = false;
			return b;
		}

		static long[] PrimesF64(long m, long M)
		{
			var rM = (long)Math.Sqrt(M);
			var m2 = Math.Max(m, rM + 1);
			var b = new bool[rM + 1]; b[2] = true;
			var b2 = new bool[M - m2 + 1];
			for (var i = 3; i <= rM; i += 2) b[i] = true;
			for (var i = m2 % 2 == 1 ? m2 : m2 + 1; i <= M; i += 2) b2[i - m2] = true;
			for (long p = 3; p <= rM; p++)
				if (b[p])
				{
					for (var i = p * p; i <= rM; i += 2 * p) b[i] = false;
					var s2 = (m2 - 1) / p;
					s2 = (s2 + 1 + s2 % 2) * p;
					for (var i = s2; i <= M; i += 2 * p) b2[i - m2] = false;
				}
			var ps2 = Range(m2, M).Where(i => b2[i - m2]).ToArray();
			return m2 == m ? ps2 : Range(m, rM).Where(i => b[i]).Concat(ps2).ToArray();
		}

		static int[] Primes(int m, int M) => Primes(M).SkipWhile(i => i < m).ToArray();
		static List<int> Primes(int M)
		{
			var ps = new List<int>();
			for (int i = 2, ri = 1; i <= M; ri = (int)Math.Sqrt(++i)) if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
			return ps;
		}

		// m >= 2
		static long[] Primes64(long m, long M)
		{
			var ps = new List<long>();
			var rM = Math.Min((long)Math.Sqrt(M), m - 1);
			for (long i = 2; i <= rM; AddPrime(ps, i++)) ;
			for (var i = m; i <= M; AddPrime(ps, i++)) ;
			return ps.SkipWhile(i => i < m).ToArray();
		}
		static void AddPrime(List<long> ps, long i)
		{
			var ri = (long)Math.Sqrt(i);
			if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
		}

		static List<int> PrimesL(int M)
		{
			var rM = (int)Math.Sqrt(M);
			var ps = new List<int>();
			var l = Enumerable.Range(2, M - 1).ToList();
			for (var i = 0; l.Count > 0 && l[0] <= rM; i++)
			{
				ps.Add(l[0]);
				l.RemoveAll(j => j % ps[i] == 0);
			}
			l.InsertRange(0, ps);
			return l;
		}

		static List<long> PrimesL64(long m, long M)
		{
			var rM = (long)Math.Sqrt(M);
			var ps = new List<long>();
			var l = Range(2, Math.Min(rM, m - 1)).ToList();
			l.AddRange(Range(m, M));
			for (var i = 0; l.Count > 0 && l[0] <= rM; i++)
			{
				// p^2 未満の判定を無視できないため非効率です。
				ps.Add(l[0]);
				l.RemoveAll(j => j % ps[i] == 0);
			}
			l.InsertRange(0, ps.SkipWhile(p => p < m));
			return l;
		}

		#region Test Methods

		[TestMethod]
		public void PrimesF()
		{
			Console.WriteLine(string.Join(" ", PrimesF(2, 100)));
			Assert.AreEqual(25, PrimesF(2, 100).Length);
			Assert.AreEqual(168, PrimesF(2, 1000).Length);
			Assert.AreEqual(1229, PrimesF(2, 10000).Length);
			Assert.AreEqual(9592, PrimesF(2, 100000).Length);
			Assert.AreEqual(78498, PrimesF(2, 1000000).Length);
		}

		[TestMethod]
		public void PrimesF_Large()
		{
			Console.WriteLine(string.Join(" ", PrimesF(10000000, 10000100)));
		}

		[TestMethod]
		public void PrimesF64()
		{
			Console.WriteLine(string.Join(" ", PrimesF64(2, 100)));
			Assert.AreEqual(25, PrimesF64(2, 100).Length);
			Assert.AreEqual(143, PrimesF64(100, 1000).Length);
			Assert.AreEqual(1061, PrimesF64(1000, 10000).Length);
			Assert.AreEqual(8363, PrimesF64(10000, 100000).Length);
			Console.WriteLine(string.Join(" ", PrimesF64(998244300, 998244400)));
			Console.WriteLine(string.Join(" ", PrimesF64(999999900, 1000000100)));
		}

		[TestMethod]
		public void PrimesF64_Large()
		{
			Console.WriteLine(string.Join(" ", PrimesF64(999999999900, 1000000000100)));
		}

		[TestMethod]
		public void Primes()
		{
			Console.WriteLine(string.Join(" ", Primes(100)));
			Assert.AreEqual(25, Primes(100).Count);
			Assert.AreEqual(168, Primes(1000).Count);
			Assert.AreEqual(1229, Primes(10000).Count);
			Assert.AreEqual(9592, Primes(100000).Count);
		}

		[TestMethod]
		public void Primes_Large()
		{
			Console.WriteLine(string.Join(" ", Primes(1000000, 1000100)));
		}

		[TestMethod]
		public void Primes64()
		{
			Console.WriteLine(string.Join(" ", Primes64(2, 100)));
			Assert.AreEqual(25, Primes64(2, 100).Length);
			Assert.AreEqual(143, Primes64(100, 1000).Length);
			Assert.AreEqual(1061, Primes64(1000, 10000).Length);
			Assert.AreEqual(8363, Primes64(10000, 100000).Length);
			Console.WriteLine(string.Join(" ", Primes64(998244300, 998244400)));
			Console.WriteLine(string.Join(" ", Primes64(999999900, 1000000100)));
		}

		[TestMethod]
		public void Primes64_Large()
		{
			Console.WriteLine(string.Join(" ", Primes64(999999999900, 1000000000100)));
		}

		[TestMethod]
		public void PrimesL()
		{
			Console.WriteLine(string.Join(" ", PrimesL(100)));
			Assert.AreEqual(25, PrimesL(100).Count);
			Assert.AreEqual(168, PrimesL(1000).Count);
			Assert.AreEqual(1229, PrimesL(10000).Count);
			Assert.AreEqual(9592, PrimesL(100000).Count);
		}

		[TestMethod]
		public void PrimesL_Large()
		{
			PrimesL(1000000);
		}

		[TestMethod]
		public void PrimesL64()
		{
			Console.WriteLine(string.Join(" ", PrimesL64(2, 100)));
			Assert.AreEqual(25, PrimesL64(2, 100).Count);
			Assert.AreEqual(143, PrimesL64(100, 1000).Count);
			Assert.AreEqual(1061, PrimesL64(1000, 10000).Count);
			Assert.AreEqual(8363, PrimesL64(10000, 100000).Count);
			Console.WriteLine(string.Join(" ", PrimesL64(998244300, 998244400)));
			Console.WriteLine(string.Join(" ", PrimesL64(999999900, 1000000100)));
		}
		#endregion
	}
}
