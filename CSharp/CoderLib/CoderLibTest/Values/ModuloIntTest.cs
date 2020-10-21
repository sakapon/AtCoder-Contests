using System;
using CoderLib6.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Values
{
	[TestClass]
	public class ModuloIntTest
	{
		[TestMethod]
		public void Ctor()
		{
			Assert.AreEqual(999999999, new MInt(-8).V);
		}

		[TestMethod]
		public void Pow()
		{
			for (var i = 1; i <= 100000; i++)
				Assert.AreEqual(1, new MInt(i).Pow(1000000006).V);
		}

		[TestMethod]
		public void Inv()
		{
			for (var i = 1; i <= 100000; i++)
				Assert.AreEqual(1, (new MInt(i).Inv() * i).V);
		}

		[TestMethod]
		public void Div()
		{
			Assert.AreEqual(312500008, ((MInt)93) / 16);
		}
	}
}
