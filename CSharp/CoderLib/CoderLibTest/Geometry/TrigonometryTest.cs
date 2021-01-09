using System;
using System.Collections.Generic;
using CoderLib8.Geometry;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Geometry
{
	[TestClass]
	public class TrigonometryTest
	{
		[TestMethod]
		public void Area()
		{
			var Test = TestHelper.CreateAreNearlyEqual<double, double, double, double>(Trigonometry.Area);

			Test(5, 6, 7, 6 * Math.Sqrt(6));
			Test(Math.Sqrt(5), Math.Sqrt(7), 3, Math.Sqrt(131) / 4);
		}
	}
}
