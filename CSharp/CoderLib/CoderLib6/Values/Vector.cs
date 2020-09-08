using System;

namespace CoderLib6.Values
{
	struct V : IEquatable<V>
	{
		public static V Zero = new V();
		public static V UnitX = new V(1, 0);
		public static V UnitY = new V(0, 1);

		public double X, Y;
		public V(double x, double y) { X = x; Y = y; }
		public override string ToString() => $"{X:F9} {Y:F9}";
		public static V Parse(string s) => Array.ConvertAll(s.Split(), double.Parse);

		public static implicit operator V(double[] v) => new V(v[0], v[1]);
		public static explicit operator double[](V v) => new[] { v.X, v.Y };

		public bool Equals(V other) => X == other.X && Y == other.Y;
		public static bool operator ==(V v1, V v2) => v1.Equals(v2);
		public static bool operator !=(V v1, V v2) => !v1.Equals(v2);
		public override bool Equals(object obj) => obj is V && Equals((V)obj);
		public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

		public static V operator +(V v) => v;
		public static V operator -(V v) => new V(-v.X, -v.Y);
		public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
		public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
		public static V operator *(double c, V v) => v * c;
		public static V operator *(V v, double c) => new V(v.X * c, v.Y * c);
		public static V operator /(V v, double c) => new V(v.X / c, v.Y / c);

		public double NormL1 => Math.Abs(X) + Math.Abs(Y);
		public double NormL2 => Math.Sqrt(X * X + Y * Y);
		public double Angle => Math.Atan2(Y, X);
		public double Cos => X / NormL2;
		public double Sin => Y / NormL2;
		public double Tan => Y / X;

		public V Normalize() => this / NormL2;
		public V Rotate(double angle)
		{
			var cos = Math.Cos(angle);
			var sin = Math.Sin(angle);
			return new V(cos * X - sin * Y, sin * X + cos * Y);
		}

		public static double Dot(V v1, V v2) => v1.X * v2.X + v1.Y * v2.Y;
		public static double Area(V v1, V v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y) / 2;
		public static V Divide(V v1, V v2, double m, double n) => (n * v1 + m * v2) / (m + n);
	}
}
