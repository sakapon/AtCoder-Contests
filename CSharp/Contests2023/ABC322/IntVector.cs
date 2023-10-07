using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Values
{
	// 任意次元ベクトル
	public class IntVector : IEquatable<IntVector>
	{
		readonly long[] v;
		public long[] Raw => v;
		public long this[int i] => v[i];
		public int Dimension => v.Length;

		public IntVector(int n) { v = new long[n]; }
		public IntVector(long[] v) { this.v = v; }
		public override string ToString() => string.Join(" ", v);
		public static IntVector Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

		public static implicit operator IntVector(long[] v) => new IntVector(v);
		public static explicit operator long[](IntVector v) => v.v;

		#region Equality Operators
		public bool Equals(IntVector other) => !(other is null) && v.SequenceEqual(other.v);
		public static bool Equals(IntVector v1, IntVector v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(IntVector v1, IntVector v2) => Equals(v1, v2);
		public static bool operator !=(IntVector v1, IntVector v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as IntVector);
		public override int GetHashCode() => v.Length == 0 ? 0 : v[0].GetHashCode();
		#endregion

		public static IntVector operator -(IntVector v) => Array.ConvertAll(v.v, x => -x);
		public static IntVector operator +(IntVector v1, IntVector v2) => v1.v.Zip(v2.v, (x, y) => x + y).ToArray();
		public static IntVector operator -(IntVector v1, IntVector v2) => v1.v.Zip(v2.v, (x, y) => x - y).ToArray();

		public long NormL1 => v.Sum(x => Math.Abs(x));
		public double Norm => Math.Sqrt(v.Sum(x => x * x));

		public static long Dot(IntVector v1, IntVector v2) => v1.v.Zip(v2.v, (x, y) => x * y).Sum();
		public static bool IsOrthogonal(IntVector v1, IntVector v2) => Dot(v1, v2) == 0;
	}
}
