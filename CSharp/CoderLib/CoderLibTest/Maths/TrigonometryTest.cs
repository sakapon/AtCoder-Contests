using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Maths
{
	[TestClass]
	public class TrigonometryTest
	{
		// 余弦定理
		static double CosA(double a, double b, double c) => (b * b + c * c - a * a) / (2 * b * c);
		// 正弦定理
		static double SinA(double a, double r) => a / (2 * r);
		// ヘロンの公式
		static double Area(double a, double b, double c)
		{
			var s = (a + b + c) / 2;
			return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
		}

		#region Test Methods

		static void AssertNearlyEqual(double expected, double actual) =>
			Assert.AreEqual(0.0, Math.Round(expected - actual, 12));

		[TestMethod]
		public void Area()
		{
			AssertNearlyEqual(6 * Math.Sqrt(6), Area(5, 6, 7));
			AssertNearlyEqual(Math.Sqrt(131) / 4, Area(Math.Sqrt(5), Math.Sqrt(7), 3));
		}
		#endregion
	}
}
