using System;

namespace CoderLib6.Values
{
	static class ArrayVector2Helper
	{
		static int[] Add(int[] v1, int[] v2) => new[] { v1[0] + v2[0], v1[1] + v2[1] };
		static int[] Subtract(int[] v1, int[] v2) => new[] { v1[0] - v2[0], v1[1] - v2[1] };

		static int NormL1(int[] v) => Math.Abs(v[0]) + Math.Abs(v[1]);
		static double NormL1(double[] v) => Math.Abs(v[0]) + Math.Abs(v[1]);

		static double NormL2(int[] v) => Math.Sqrt(v[0] * v[0] + v[1] * v[1]);
		static double NormL2(double[] v) => Math.Sqrt(v[0] * v[0] + v[1] * v[1]);

		static double Distance(int[] p, int[] q) => Math.Sqrt((p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]));
		static double Distance(double[] p, double[] q) => Math.Sqrt((p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]));
	}
}
