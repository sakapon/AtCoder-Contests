using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class BinarySearchTest
	{
		[TestMethod]
		public void Index_Int()
		{
			var a = new[] { 3, 5, 6, 6, 6, 8 };
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(Array.BinarySearch(a, i), BinarySearch.IndexOf(a, i));
		}

		[TestMethod]
		public void IndexForInsert_Int()
		{
			var a = new[] { 3, 5, 6, 6, 6, 8 };
			Assert.AreEqual(0, BinarySearch.IndexForInsert(a, 1));
			Assert.AreEqual(0, BinarySearch.IndexForInsert(a, 2));
			Assert.AreEqual(1, BinarySearch.IndexForInsert(a, 3));
			Assert.AreEqual(1, BinarySearch.IndexForInsert(a, 4));
			Assert.AreEqual(2, BinarySearch.IndexForInsert(a, 5));
			Assert.AreEqual(5, BinarySearch.IndexForInsert(a, 6));
			Assert.AreEqual(5, BinarySearch.IndexForInsert(a, 7));
			Assert.AreEqual(6, BinarySearch.IndexForInsert(a, 8));
			Assert.AreEqual(6, BinarySearch.IndexForInsert(a, 9));
		}

		[TestMethod]
		public void Index_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Index_Random(n);
				for (int n = 1000; n < 1010; n++) Index_Random(n);
			}
		}

		static void Index_Random(int n)
		{
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = BinarySearch.IndexOf(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual]);
					Assert.IsTrue(actual == 0 || a[actual - 1] < i);
				}
				else
				{
					Assert.AreEqual(expected, actual);
				}
			}
		}

		[TestMethod]
		public void IndexForInsert_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) IndexForInsert_Random(n);
				for (int n = 1000; n < 1010; n++) IndexForInsert_Random(n);
			}
		}

		static void IndexForInsert_Random(int n)
		{
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = BinarySearch.IndexForInsert(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual - 1]);
					Assert.IsTrue(actual == n || a[actual] > i);
				}
				else
				{
					Assert.AreEqual(~expected, actual);
				}
			}
		}

		[TestMethod]
		public void Index_Time()
		{
			var n = 300000;
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			var r = TimeHelper.Measure(() => Enumerable.Range(0, n).Select(x => BinarySearch.IndexOf(a, x)).ToArray());
		}

		[TestMethod]
		public void IndexForInsert_Time()
		{
			var n = 300000;
			var a = RandomHelper.CreateData(n).OrderBy(x => x).ToArray();
			var r = TimeHelper.Measure(() => Enumerable.Range(0, n).Select(x => BinarySearch.IndexForInsert(a, x)).ToArray());
		}
	}
}
