using System;
using System.Linq;

namespace CoderLib6.Values
{
	static class ArrayVectorHelper
	{
		static int[] Add(int[] v1, int[] v2) => v1.Zip(v2, (x, y) => x + y).ToArray();
		static int[] Subtract(int[] v1, int[] v2) => v1.Zip(v2, (x, y) => x - y).ToArray();

		static int NormL1(int[] v) => v.Sum(Math.Abs);
		static double NormL2(int[] v) => Math.Sqrt(v.Sum(x => x * x));
		// Minkowski Distance
		static double Norm(int[] v, double p) => Math.Pow(v.Sum(x => Math.Pow(Math.Abs(x), p)), 1 / p);
		static int NormMax(int[] v) => v.Max(Math.Abs);
	}
}
