using System;
using System.Linq;

namespace CoderLib6.Values
{
	// 任意次元ベクトル
	static class ArrayVectorHelper
	{
		static long[] Negate(long[] v) => Array.ConvertAll(v, x => -x);
		static long[] Add(long[] v1, long[] v2) => v1.Zip(v2, (x, y) => x + y).ToArray();
		static long[] Subtract(long[] v1, long[] v2) => v1.Zip(v2, (x, y) => x - y).ToArray();

		static long NormL1(long[] v) => v.Sum(x => Math.Abs(x));
		static double NormL2(long[] v) => Math.Sqrt(v.Sum(x => x * x));
		// Minkowski Distance
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/2/ITP1/10/ITP1_10_D
		static double Norm(long[] v, double p) => Math.Pow(v.Sum(x => Math.Pow(Math.Abs(x), p)), 1 / p);
		static long NormMax(long[] v) => v.Max(x => Math.Abs(x));

		static long Dot(long[] v1, long[] v2) => v1.Zip(v2, (x, y) => x * y).Sum();
		static bool IsOrthogonal(long[] v1, long[] v2) => Dot(v1, v2) == 0;
	}
}
