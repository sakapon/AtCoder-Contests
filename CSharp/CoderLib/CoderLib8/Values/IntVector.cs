using System;
using System.Collections.Generic;
using System.Linq;

// Test: https://atcoder.jp/contests/abc322/tasks/abc322_e
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
		public bool Equals(IntVector other)
		{
			if (other is null) return false;
			if (v.Length != other.v.Length) return false;
			for (int i = 0; i < v.Length; ++i)
				if (v[i] != other.v[i]) return false;
			return true;
		}
		public static bool Equals(IntVector v1, IntVector v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(IntVector v1, IntVector v2) => Equals(v1, v2);
		public static bool operator !=(IntVector v1, IntVector v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as IntVector);
		public override int GetHashCode() =>
			v.Length == 0 ? 0 :
			v.Length == 1 ? v[0].GetHashCode() :
			v.Length == 2 ? HashCode.Combine(v[0], v[1]) :
			v.Length == 3 ? HashCode.Combine(v[0], v[1], v[2]) :
			v.Length == 4 ? HashCode.Combine(v[0], v[1], v[2], v[3]) :
			HashCode.Combine(v[0], v[1], v[2], v[3], v[4]);
		#endregion

		public static IntVector operator -(IntVector v) => Array.ConvertAll(v.v, x => -x);
		public static IntVector operator +(IntVector v1, IntVector v2) => new IntVector(Add(v1.v, v2.v));
		public static IntVector operator -(IntVector v1, IntVector v2) => new IntVector(Subtract(v1.v, v2.v));

		public long NormL1 => v.Sum(x => Math.Abs(x));
		public double Norm => Math.Sqrt(v.Sum(x => x * x));

		public static long Dot(IntVector v1, IntVector v2) => v1.v.Zip(v2.v, (x, y) => x * y).Sum();
		public static bool IsOrthogonal(IntVector v1, IntVector v2) => Dot(v1, v2) == 0;

		static long[] Add(long[] v1, long[] v2)
		{
			var r = new long[v1.Length];
			for (int i = 0; i < v1.Length; ++i)
				r[i] = v1[i] + v2[i];
			return r;
		}
		static long[] Subtract(long[] v1, long[] v2)
		{
			var r = new long[v1.Length];
			for (int i = 0; i < v1.Length; ++i)
				r[i] = v1[i] - v2[i];
			return r;
		}
	}
}
