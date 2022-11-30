using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerTest.Page001
{
	[TestClass]
	public class P001_010
	{
		#region Test Methods
		[TestMethod] public void T001() => TestHelper.Execute();
		[TestMethod] public void T002() => TestHelper.Execute();
		[TestMethod] public void T003() => TestHelper.Execute();
		[TestMethod] public void T004() => TestHelper.Execute();
		[TestMethod] public void T005() => TestHelper.Execute();
		[TestMethod] public void T006() => TestHelper.Execute();
		[TestMethod] public void T007() => TestHelper.Execute();
		[TestMethod] public void T008() => TestHelper.Execute();
		[TestMethod] public void T009() => TestHelper.Execute();
		[TestMethod] public void T010() => TestHelper.Execute();
		#endregion

		public static object P001()
		{
			const int n = 1000;
			return Enumerable.Range(0, n).Where(x => x % 3 == 0 || x % 5 == 0).Sum();
		}

		public static object P002()
		{
			const int n = 4000000;
			return Fibonacci.FibonacciSeq(1, 2).TakeWhile(x => x <= n).Where(x => x % 2 == 0).Sum(x => (int)x);
		}

		public static object P003()
		{
			const long n = 600851475143;
			return Primes.Factorize(n)[^1];
		}

		public static object P004()
		{
			return 0;
		}

		public static object P005()
		{
			return 16 * 9 * 5 * 7 * 11 * 13 * 17 * 19;
		}

		public static object P006()
		{
			const int n = 100;
			var ns = n * (n + 1) / 2;
			var ns2 = n * (n + 1) * (2 * n + 1) / 6;
			return ns * ns - ns2;
		}

		public static object P007()
		{
			const int n = 200000;
			var ps = Primes.GetPrimes(n);
			return ps[10000];
		}

		public static object P008()
		{
			return 0;
		}

		public static object P009()
		{
			const int s = 1000;

			for (int m = 1; m < 10; m++)
			{
				for (int n = 1; n < m; n++)
				{
					if ((m & 1) == (n & 1)) continue;

					var a = m * m - n * n;
					var b = 2 * m * n;
					var c = m * m + n * n;
					//Console.WriteLine($"{a} {b} {c}");

					var q = Math.DivRem(s, a + b + c, out var rem);
					if (rem == 0) return q * q * q * a * b * c;
				}
			}
			return 0;
		}

		public static object P010()
		{
			const int n = 2000000;
			return Primes.GetPrimes(n).Sum(x => (long)x);
		}
	}
}
