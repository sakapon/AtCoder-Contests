using System;
using V2 = System.ValueTuple<long, long>;

namespace CoderLib8.Values
{
	public static class TupleVector2Ex
	{
		public static V2 Negate(this V2 v) => (-v.Item1, -v.Item2);
		public static V2 Add(this V2 v1, V2 v2) => (v1.Item1 + v2.Item1, v1.Item2 + v2.Item2);
		public static V2 Subtract(this V2 v1, V2 v2) => (v1.Item1 - v2.Item1, v1.Item2 - v2.Item2);

		public static long NormL1(this V2 v) => Math.Abs(v.Item1) + Math.Abs(v.Item2);
		public static long NormL2Squared(this V2 v) => v.Item1 * v.Item1 + v.Item2 * v.Item2;
		public static double NormL2(this V2 v) => Math.Sqrt(v.Item1 * v.Item1 + v.Item2 * v.Item2);

		public static long DistanceSquared(this V2 v1, V2 v2) => NormL2Squared(Subtract(v1, v2));
		public static double Distance(this V2 v1, V2 v2) => NormL2(Subtract(v1, v2));
	}
}
