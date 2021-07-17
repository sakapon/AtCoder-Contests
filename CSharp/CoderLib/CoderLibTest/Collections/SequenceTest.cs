using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class SequenceTest
	{
		[TestMethod]
		public void CumSum()
		{
			Assert.AreEqual(500500, SeqHelper.CumSum(Enumerable.Range(1, 1000).ToArray())[1000]);
			Assert.AreEqual(5000050000, SeqHelper.CumSumL(Enumerable.Range(1, 100000).ToArray())[100000]);
		}

		[TestMethod]
		public void Pows()
		{
			CollectionAssert.AreEqual(SeqHelper.Pows2(), SeqHelper.Pows(2, 30));
			CollectionAssert.AreEqual(SeqHelper.Pows2L(), SeqHelper.PowsL(2, 62));
			Assert.AreEqual(4052555153018976267, SeqHelper.PowsL(3, 39)[39]);
		}

		[TestMethod]
		public void SlideMin()
		{
			CollectionAssert.AreEqual(new[] { 5, 6, 7 }, SeqHelper.SlideMin(new[] { 5, 6, 7, 8, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 7, 6, 5 }, SeqHelper.SlideMin(new[] { 9, 8, 7, 6, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 5, 5 }, SeqHelper.SlideMin(new[] { 7, 6, 8, 5, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 6, 5 }, SeqHelper.SlideMin(new[] { 7, 8, 6, 9, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 5, 5, 5 }, SeqHelper.SlideMin(new[] { 9, 7, 5, 6, 8 }, 3));
			CollectionAssert.AreEqual(new[] { 6, 7, 5 }, SeqHelper.SlideMin(new[] { 6, 8, 9, 7, 5 }, 3));
		}

		[TestMethod]
		public void SlideMax()
		{
			CollectionAssert.AreEqual(new[] { 7, 8, 9 }, SeqHelper.SlideMax(new[] { 5, 6, 7, 8, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 8, 7 }, SeqHelper.SlideMax(new[] { 9, 8, 7, 6, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 8, 8, 9 }, SeqHelper.SlideMax(new[] { 7, 6, 8, 5, 9 }, 3));
			CollectionAssert.AreEqual(new[] { 8, 9, 9 }, SeqHelper.SlideMax(new[] { 7, 8, 6, 9, 5 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 7, 8 }, SeqHelper.SlideMax(new[] { 9, 7, 5, 6, 8 }, 3));
			CollectionAssert.AreEqual(new[] { 9, 9, 9 }, SeqHelper.SlideMax(new[] { 6, 8, 9, 7, 5 }, 3));
		}
	}
}
