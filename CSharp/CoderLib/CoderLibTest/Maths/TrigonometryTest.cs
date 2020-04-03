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
	}
}
