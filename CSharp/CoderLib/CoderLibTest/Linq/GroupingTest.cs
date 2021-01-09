using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Linq
{
	[TestClass]
	public class GroupingTest
	{
		[TestMethod]
		public void GroupBySeq_1()
		{
			var actual = Enumerable.Range(0, 9).GroupBySeq(i => i % 4 == 0).ToArray();

			CollectionAssert.AreEqual(new[] { true, false, true, false, true }, actual.Select(g => g.Key).ToArray());
			CollectionAssert.AreEqual(new[] { 1, 3, 1, 3, 1 }, actual.Select(g => g.Count()).ToArray());
		}

		[TestMethod]
		public void GroupBySeq_2()
		{
			var actual = Enumerable.Range(1, 11).GroupBySeq(i => i % 4 == 0).ToArray();

			CollectionAssert.AreEqual(new[] { false, true, false, true, false }, actual.Select(g => g.Key).ToArray());
			CollectionAssert.AreEqual(new[] { 3, 1, 3, 1, 3 }, actual.Select(g => g.Count()).ToArray());
		}

		[TestMethod]
		public void GroupBySeq_Empty()
		{
			var empty = new int[0];

			CollectionAssert.AreEqual(empty, empty.GroupBySeq(i => i).ToArray());
		}
	}
}
