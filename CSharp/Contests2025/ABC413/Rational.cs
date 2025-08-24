using System;

namespace CoderLib8.Values
{
	public struct Rational : IEquatable<Rational>, IComparable<Rational>
	{
		// X / Y
		// 通分はしません。
		public long X, Y;
		public Rational(long x, long y) { if (y < 0) { x = -x; y = -y; } X = x; Y = y; }
		public override string ToString() => $"{X} / {Y}";
		public static implicit operator Rational(long v) => new Rational(v, 1);

		public bool Equals(Rational other) => X * other.Y == other.X * Y;
		public static bool operator ==(Rational v1, Rational v2) => v1.Equals(v2);
		public static bool operator !=(Rational v1, Rational v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is Rational v && Equals(v);
		public override int GetHashCode() => (X, Y).GetHashCode();

		public int CompareTo(Rational other) => (X * other.Y).CompareTo(other.X * Y);
		public static bool operator <(Rational v1, Rational v2) => v1.CompareTo(v2) < 0;
		public static bool operator >(Rational v1, Rational v2) => v1.CompareTo(v2) > 0;
		public static bool operator <=(Rational v1, Rational v2) => v1.CompareTo(v2) <= 0;
		public static bool operator >=(Rational v1, Rational v2) => v1.CompareTo(v2) >= 0;

		public static Rational operator -(Rational v) => new Rational(-v.X, v.Y);
	}
}
