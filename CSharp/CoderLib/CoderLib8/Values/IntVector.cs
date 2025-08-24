using System;
using System.Collections.Generic;
using System.Linq;

// Test: https://atcoder.jp/contests/abc322/tasks/abc322_e
namespace CoderLib8.Values
{
	// 任意次元ベクトル
	public class IntVector : IEquatable<IntVector>
	{
		// 下位ビットが分散するようにハッシュを生成します。
		public static int GetHashCode(long[] a)
		{
			var h = 0;
			for (int i = 0; i < a.Length; ++i) h = h * 987654323 + a[i].GetHashCode();
			return h;
		}

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
		public bool Equals(IntVector other) => !(other is null) && Equals(v, other.v);
		public static bool Equals(IntVector v1, IntVector v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(IntVector v1, IntVector v2) => Equals(v1, v2);
		public static bool operator !=(IntVector v1, IntVector v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as IntVector);
		public override int GetHashCode() => GetHashCode(v);

		public static bool Equals(long[] a1, long[] a2)
		{
			if (a1.Length != a2.Length) return false;
			for (int i = 0; i < a1.Length; ++i)
				if (a1[i] != a2[i]) return false;
			return true;
		}
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
