﻿using System;
using System.Collections.Generic;
using CoderLib6.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class SequenceTest
	{
		[TestMethod]
		public void Sum()
		{
			var seq = new Seq(new[] { 1, 2, 3, 4, 5 });

			Assert.AreEqual(1, seq.Sum(0, 1));
			Assert.AreEqual(3, seq.Sum(2, 3));
			Assert.AreEqual(9, seq.Sum(1, 4));
			Assert.AreEqual(14, seq.Sum(1, 5));
			Assert.AreEqual(15, seq.Sum(0, 5));
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
