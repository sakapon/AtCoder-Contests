using System;

namespace CoderLib8.Values
{
	// 偏角による順序
	// https://atcoder.jp/contests/abc225/tasks/abc225_e
	public struct IntV : IEquatable<IntV>
	{
		public static IntV Zero = (0, 0);
		public static IntV UnitX = (1, 0);
		public static IntV UnitY = (0, 1);

		public long X, Y;
		public IntV(long x, long y) => (X, Y) = (x, y);
		public void Deconstruct(out long x, out long y) => (x, y) = (X, Y);
		public override string ToString() => $"{X} {Y}";
		public static IntV Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

		public static implicit operator IntV(long[] v) => (v[0], v[1]);
		public static explicit operator long[](IntV v) => new[] { v.X, v.Y };
		public static implicit operator IntV((long x, long y) v) => new IntV(v.x, v.y);
		public static explicit operator (long, long)(IntV v) => (v.X, v.Y);

		public bool Equals(IntV other) => X == other.X && Y == other.Y;
		public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
		public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is IntV v && Equals(v);
		public override int GetHashCode() => (X, Y).GetHashCode();

		public static IntV operator -(IntV v) => (-v.X, -v.Y);
		public static IntV operator +(IntV v1, IntV v2) => (v1.X + v2.X, v1.Y + v2.Y);
		public static IntV operator -(IntV v1, IntV v2) => (v1.X - v2.X, v1.Y - v2.Y);

		public long NormL1 => Math.Abs(X) + Math.Abs(Y);
		public double Norm => Math.Sqrt(X * X + Y * Y);
		public double Angle => Math.Atan2(Y, X);
		public double Cos => X / Norm;
		public double Sin => Y / Norm;
		public double Tan => Y / (double)X;

		public IntV Rotate90() => new IntV(-Y, X);
		public IntV Rotate180() => new IntV(-X, -Y);
		public IntV Rotate270() => new IntV(Y, -X);

		public static long Dot(IntV v1, IntV v2) => v1.X * v2.X + v1.Y * v2.Y;
		// 菱形の面積
		public static long Area(IntV v1, IntV v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y);
		public static bool IsParallel(IntV v1, IntV v2) => v1.X * v2.Y == v2.X * v1.Y;
		public static bool IsOrthogonal(IntV v1, IntV v2) => Dot(v1, v2) == 0;
	}
}
