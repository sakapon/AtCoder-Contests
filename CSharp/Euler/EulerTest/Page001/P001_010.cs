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
		[TestMethod] public void Test001() => TestHelper.Execute();
		[TestMethod] public void Test002() => TestHelper.Execute();
		[TestMethod] public void Test003() => TestHelper.Execute();
		[TestMethod] public void Test004() => TestHelper.Execute();
		[TestMethod] public void Test005() => TestHelper.Execute();
		[TestMethod] public void Test006() => TestHelper.Execute();
		[TestMethod] public void Test007() => TestHelper.Execute();
		[TestMethod] public void Test008() => TestHelper.Execute();
		[TestMethod] public void Test009() => TestHelper.Execute();
		[TestMethod] public void Test010() => TestHelper.Execute();
		#endregion

		public static object P001()
		{
			const int n = 1000;
			return Enumerable.Range(0, n).Where(x => x % 3 == 0 || x % 5 == 0).Sum();
		}

		public static object P002()
		{
			const int n = 4000000;

			var r = 2;
			var (t1, t2) = (1, 2);
			int t;

			while ((t = t1 + t2) <= n)
			{
				if (t % 2 == 0) r += t;
				(t1, t2) = (t2, t);
			}
			return r;
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
			return 0;
		}

		public static object P007()
		{
			var ps = Primes.GetPrimes(200000);
			return ps[10000];
		}

		public static object P008()
		{
			return 0;
		}

		public static object P009()
		{
			return 0;
		}

		public static object P010()
		{
			return 0;
		}
	}
}
