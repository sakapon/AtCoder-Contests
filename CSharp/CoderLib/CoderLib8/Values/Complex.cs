using System;

namespace CoderLib8.Values
{
	struct Comp : IEquatable<Comp>
	{
		public double X, Y;
		public Comp(double x, double y) => (X, Y) = (x, y);
		public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
		public override string ToString() => $"{X:F9} {Y:F9}";

		public static implicit operator Comp(int v) => new Comp(v, 0);
		public static implicit operator Comp(long v) => new Comp(v, 0);
		public static implicit operator Comp(double v) => new Comp(v, 0);

		public bool Equals(Comp other) => X == other.X && Y == other.Y;
		public static bool operator ==(Comp v1, Comp v2) => v1.Equals(v2);
		public static bool operator !=(Comp v1, Comp v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is Comp v && Equals(v);
		public override int GetHashCode() => (X, Y).GetHashCode();

		public static Comp operator -(Comp v) => new Comp(-v.X, -v.Y);
		public static Comp operator +(Comp v1, Comp v2) => new Comp(v1.X + v2.X, v1.Y + v2.Y);
		public static Comp operator -(Comp v1, Comp v2) => new Comp(v1.X - v2.X, v1.Y - v2.Y);
		public static Comp operator *(Comp v1, Comp v2) => new Comp(v1.X * v2.X - v1.Y * v2.Y, v1.X * v2.Y + v1.Y * v2.X);
		public static Comp operator *(double c, Comp v) => v * c;
		public static Comp operator *(Comp v, double c) => new Comp(v.X * c, v.Y * c);
		public static Comp operator /(Comp v, double c) => new Comp(v.X / c, v.Y / c);

		public static Comp NthRoot(int n, int i)
		{
			var angle = i * 2 * Math.PI / n;
			return new Comp(Math.Cos(angle), Math.Sin(angle));
		}

		public double NormL1 => Math.Abs(X) + Math.Abs(Y);
		public double Norm => Math.Sqrt(X * X + Y * Y);
		public double Angle => Math.Atan2(Y, X);
		public double Cos => X / Norm;
		public double Sin => Y / Norm;
		public double Tan => Y / X;

		public Comp Rotate(double angle)
		{
			var cos = Math.Cos(angle);
			var sin = Math.Sin(angle);
			return new Comp(cos * X - sin * Y, sin * X + cos * Y);
		}
	}
}
