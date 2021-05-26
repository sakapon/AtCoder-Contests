using System;
using System.Numerics;
using CoderLib8.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Numerics
{
	[TestClass]
	public class Math2Test
	{
		[TestMethod]
		public void Pow()
		{
			var b = 2;
			for (var i = 0; i <= 62; i++)
				Assert.AreEqual((long)Math.Pow(b, i), Math2.Pow(b, i));
			b = 3;
			for (var i = 0; i <= 33; i++)
				Assert.AreEqual((long)Math.Pow(b, i), Math2.Pow(b, i));
		}

		[TestMethod]
		public void GetNprs()
		{
			var npr = Math2.GetNprs();
			Assert.AreEqual(1, npr[0, 0]);
			Assert.AreEqual(1, npr[1, 0]);
			Assert.AreEqual(1, npr[1, 1]);
			Assert.AreEqual(0, npr[1, 2]);
			Assert.AreEqual(1, npr[3, 0]);
			Assert.AreEqual(3, npr[3, 1]);
			Assert.AreEqual(6, npr[3, 2]);
			Assert.AreEqual(6, npr[3, 3]);
			Assert.AreEqual(151200, npr[10, 6]);
			Assert.AreEqual(2432902008176640000, npr[20, 20]);
		}

		[TestMethod]
		public void GetNcrs()
		{
			var ncr = Math2.GetNcrs();
			Assert.AreEqual(1, ncr[0, 0]);
			Assert.AreEqual(1, ncr[1, 0]);
			Assert.AreEqual(1, ncr[1, 1]);
			Assert.AreEqual(0, ncr[1, 2]);
			Assert.AreEqual(1, ncr[3, 0]);
			Assert.AreEqual(3, ncr[3, 1]);
			Assert.AreEqual(3, ncr[3, 2]);
			Assert.AreEqual(1, ncr[3, 3]);
			Assert.AreEqual(210, ncr[10, 6]);
			Assert.AreEqual(7219428434016265740, ncr[66, 33]);
		}

		[TestMethod]
		public void Factorial()
		{
			Assert.AreEqual(1, Math2.Factorial(0));
			Assert.AreEqual(1, Math2.Factorial(1));
			Assert.AreEqual(2, Math2.Factorial(2));
			Assert.AreEqual(6, Math2.Factorial(3));
			Assert.AreEqual(24, Math2.Factorial(4));
			Assert.AreEqual(2432902008176640000, Math2.Factorial(20));
		}

		[TestMethod]
		public void Npr()
		{
			Assert.AreEqual(1, Math2.Npr(0, 0));
			Assert.AreEqual(1, Math2.Npr(1, 0));
			Assert.AreEqual(1, Math2.Npr(1, 1));
			Assert.AreEqual(0, Math2.Npr(1, 2));
			Assert.AreEqual(1, Math2.Npr(3, 0));
			Assert.AreEqual(3, Math2.Npr(3, 1));
			Assert.AreEqual(6, Math2.Npr(3, 2));
			Assert.AreEqual(6, Math2.Npr(3, 3));
			Assert.AreEqual(151200, Math2.Npr(10, 6));
		}

		[TestMethod]
		public void Ncr()
		{
			Assert.AreEqual(1, Math2.Ncr(0, 0));
			Assert.AreEqual(1, Math2.Ncr(1, 0));
			Assert.AreEqual(1, Math2.Ncr(1, 1));
			Assert.AreEqual(0, Math2.Ncr(1, 2));
			Assert.AreEqual(1, Math2.Ncr(3, 0));
			Assert.AreEqual(3, Math2.Ncr(3, 1));
			Assert.AreEqual(3, Math2.Ncr(3, 2));
			Assert.AreEqual(1, Math2.Ncr(3, 3));
			Assert.AreEqual(210, Math2.Ncr(10, 6));
		}

		[TestMethod]
		public void PopCount()
		{
			for (ulong x = 0; x < 1 << 10; x++)
				Assert.AreEqual(BitOperations.PopCount(x), Math2.PopCount(x));
			for (ulong x = 1L << 50; x < (1L << 50) + (1 << 10); x++)
				Assert.AreEqual(BitOperations.PopCount(x), Math2.PopCount(x));
		}
	}
}
