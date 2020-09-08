using System;

namespace CoderLib6.Values
{
	struct IntV : IEquatable<IntV>
	{
		public int X, Y;
		public IntV(int x, int y) { X = x; Y = y; }
		public override string ToString() => $"{X} {Y}";

		public bool Equals(IntV other) => X == other.X && Y == other.Y;
		public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
		public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is IntV && Equals((IntV)obj);
		public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

		public static IntV operator -(IntV v) => new IntV(-v.X, -v.Y);
		public static IntV operator +(IntV v1, IntV v2) => new IntV(v1.X + v2.X, v1.Y + v2.Y);
		public static IntV operator -(IntV v1, IntV v2) => new IntV(v1.X - v2.X, v1.Y - v2.Y);

		public int NormL1 => Math.Abs(X) + Math.Abs(Y);
		public double NormL2 => Math.Sqrt(X * X + Y * Y);
	}
}
