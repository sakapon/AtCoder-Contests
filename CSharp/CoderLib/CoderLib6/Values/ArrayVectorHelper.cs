using System;
using System.Linq;

namespace CoderLib6.Values
{
	// 任意次元ベクトル
	static class ArrayVectorHelper
	{
		static int[] Add(int[] v1, int[] v2) => v1.Zip(v2, (x, y) => x + y).ToArray();
		static int[] Subtract(int[] v1, int[] v2) => v1.Zip(v2, (x, y) => x - y).ToArray();

		static int NormL1(int[] v) => v.Sum(Math.Abs);
		static double NormL2(int[] v) => Math.Sqrt(v.Sum(x => x * x));
		// Minkowski Distance
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/2/ITP1/10/ITP1_10_D
		static double Norm(int[] v, double p) => Math.Pow(v.Sum(x => Math.Pow(Math.Abs(x), p)), 1 / p);
		static int NormMax(int[] v) => v.Max(Math.Abs);
	}
}
