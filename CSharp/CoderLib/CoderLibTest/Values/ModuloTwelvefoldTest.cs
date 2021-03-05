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

			Assert.AreEqual(55980, tw.Surjection(10));
			Assert.AreEqual(41, tw.Bell(5));
		}

		[TestMethod]
		public void Twelvefold5()
		{
			var tw = new MTwelvefold(5);

			Assert.AreEqual(9765625, tw.Power(10));
			Assert.AreEqual(42525, tw.Stirling(10));
		}

		[TestMethod]
		public void Factorial()
		{
			for (var k = 1; k <= 1000; k++)
			{
				var tw = new MTwelvefold(k);
				Assert.AreEqual(tw.MFactorial(k), tw.Surjection(k));
			}
		}
	}
}
