using System;

namespace CoderLib6.Values
{
	struct V : IEquatable<V>
	{
		public static V Zero { get; } = new V();
		public static V UnitX { get; } = new V(1, 0);
		public static V UnitY { get; } = new V(0, 1);

		public double X, Y;
		public V(double x, double y) { X = x; Y = y; }
		public override string ToString() => $"{X} {Y}";

		public bool Equals(V other) => X == other.X && Y == other.Y;
		public static bool operator ==(V v1, V v2) => v1.Equals(v2);
		public static bool operator !=(V v1, V v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is V && Equals((V)obj);
		public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

		public static V operator +(V v) => v;
		public static V operator -(V v) => new V(-v.X, -v.Y);
		public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
		public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
		public static V operator *(double c, V v) => new V(v.X * c, v.Y * c);
		public static V operator *(V v, double c) => new V(v.X * c, v.Y * c);
		public static V operator /(V v, double c) => new V(v.X / c, v.Y / c);

		public double NormL1 => Math.Abs(X) + Math.Abs(Y);
		public double NormL2 => Math.Sqrt(X * X + Y * Y);
		public double Angle => Math.Atan2(Y, X);

		public static double Dot(V v1, V v2) => v1.X * v2.X + v1.Y * v2.Y;
		public static double Area(V v1, V v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y) / 2;
	}
}
