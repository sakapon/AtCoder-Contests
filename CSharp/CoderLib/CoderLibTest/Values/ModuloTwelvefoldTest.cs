using System;
using CoderLib6.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Values
{
	[TestClass]
	public class ModuloTwelvefoldTest
	{
		[TestMethod]
		public void Twelvefold3()
		{
			var tw = new MTwelvefold(3);

			Assert.AreEqual(55980, tw.Surjection(10, 3));
			Assert.AreEqual(41, tw.Bell(5, 3));
		}

		[TestMethod]
		public void Twelvefold5()
		{
			var tw = new MTwelvefold(5);

			Assert.AreEqual(9765625, MTwelvefold.Way01(10, 5));
			Assert.AreEqual(42525, tw.Stirling(10, 5));
			Assert.AreEqual(30, MTwelvefold.Partition(10, 5));
			Assert.AreEqual(7, MTwelvefold.PartitionPositive(10, 5));
		}

		[TestMethod]
		public void Factorial()
		{
			var tw = new MTwelvefold(1000);
			for (var k = 1; k <= 1000; k++)
			{
				Assert.AreEqual(tw.MFactorial(k), tw.Surjection(k, k));
			}
		}
	}
}
