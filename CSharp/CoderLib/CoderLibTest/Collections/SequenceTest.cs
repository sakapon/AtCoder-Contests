using System;
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
	}
}
