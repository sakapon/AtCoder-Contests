using System;

namespace CoderLib8.Geometry
{
	static class Trigonometry
	{
		// 余弦定理
		static double CosA(double a, double b, double c) => (b * b + c * c - a * a) / (2 * b * c);
		// 正弦定理
		static double SinA(double a, double r) => a / (2 * r);
		// ヘロンの公式
		public static double Area(double a, double b, double c)
		{
			var s = (a + b + c) / 2;
			return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
		}
	}
}
