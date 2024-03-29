﻿using System;

namespace CoderLib6.Values
{
	// 偏角による順序
	// https://atcoder.jp/contests/abc225/tasks/abc225_e
	public struct IntV : IEquatable<IntV>
	{
		public static IntV Zero = new IntV();
		public static IntV UnitX = new IntV(1, 0);
		public static IntV UnitY = new IntV(0, 1);

		public long X, Y;
		public IntV(long x, long y) { X = x; Y = y; }
		public override string ToString() => $"{X} {Y}";
		public static IntV Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

		public static implicit operator IntV(long[] v) => new IntV(v[0], v[1]);
		public static explicit operator long[](IntV v) => new[] { v.X, v.Y };

		public bool Equals(IntV other) => X == other.X && Y == other.Y;
		public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
		public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is IntV && Equals((IntV)obj);
		public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

		public static IntV operator -(IntV v) => new IntV(-v.X, -v.Y);
		public static IntV operator +(IntV v1, IntV v2) => new IntV(v1.X + v2.X, v1.Y + v2.Y);
		public static IntV operator -(IntV v1, IntV v2) => new IntV(v1.X - v2.X, v1.Y - v2.Y);

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
