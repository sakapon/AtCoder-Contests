using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class ArrayTest
	{
		static int[] Range(int start, int count)
		{
			var a = new int[count];
			for (var i = 0; i < count; ++i) a[i] = start + i;
			return a;
		}

		static int[] Range2(int m, int M)
		{
			var a = new int[M - m + 1];
			for (var i = m; i <= M; ++i) a[i - m] = i;
			return a;
		}

		static int DotProduct(int[] a, int[] b)
		{
			var r = 0;
			for (var i = 0; i < a.Length; i++) r += a[i] * b[i];
			return r;
		}

		static int DotProduct2(int[] a, int[] b) { for (int r = 0, i = -1; ; r += a[i] * b[i]) if (++i >= a.Length) return r; }

		#region Test Methods

		[TestMethod]
		public void Range()
		{
			CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Range(0, 5));
			CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, Range(2, 5));
		}

		[TestMethod]
		public void Range2()
		{
			CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Range2(0, 4));
			CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, Range2(2, 6));
		}

		[TestMethod]
		public void DotProduct()
		{
			Assert.AreEqual(32, DotProduct(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
			Assert.AreEqual(89, DotProduct(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
		}
		#endregion
	}
}
