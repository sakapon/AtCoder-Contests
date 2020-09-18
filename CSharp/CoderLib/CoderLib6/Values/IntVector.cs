using System;
using System.Linq;

namespace CoderLib6.Values
{
	// 任意次元ベクトル
	// 渡された配列をラップするだけです。
	class IntVector
	{
		public long[] V;
		public IntVector(long[] v) { V = v; }
		public override string ToString() => string.Join(" ", V);
		public static IntVector Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

		public static implicit operator IntVector(long[] v) => new IntVector(v);
		public static explicit operator long[](IntVector v) => v.V;

		public static IntVector operator -(IntVector v) => Array.ConvertAll(v.V, x => -x);
		public static IntVector operator +(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x + y).ToArray();
		public static IntVector operator -(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x - y).ToArray();

		public long NormL1 => V.Sum(x => Math.Abs(x));
		public double Norm => Math.Sqrt(V.Sum(x => x * x));

		public static long Dot(IntVector v1, IntVector v2) => v1.V.Zip(v2.V, (x, y) => x * y).Sum();
		public static bool IsOrthogonal(IntVector v1, IntVector v2) => Dot(v1, v2) == 0;
	}
}
