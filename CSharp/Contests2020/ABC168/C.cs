using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (a, b, h, m) = Read4();

		V v1 = (a, 0);
		V v2 = (b, 0);
		v1 = v1.Rotate(2 * Math.PI * (60 * h + m) / 720);
		v2 = v2.Rotate(2 * Math.PI * m / 60);
		Console.WriteLine((v1 - v2).Norm);
	}
}

struct V : IEquatable<V>
{
	public double X, Y;
	public V(double x, double y) => (X, Y) = (x, y);
	public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
	public override string ToString() => $"{X:F9} {Y:F9}";
	public static V Parse(string s) => Array.ConvertAll(s.Split(), double.Parse);

	public static implicit operator V(double[] v) => (v[0], v[1]);
	public static explicit operator double[](V v) => new[] { v.X, v.Y };
	public static implicit operator V((double x, double y) v) => new V(v.x, v.y);
	public static explicit operator (double, double)(V v) => (v.X, v.Y);

	public bool Equals(V other) => X == other.X && Y == other.Y;
	public static bool operator ==(V v1, V v2) => v1.Equals(v2);
	public static bool operator !=(V v1, V v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is V v && Equals(v);
	public override int GetHashCode() => (X, Y).GetHashCode();

	public static V operator -(V v) => (-v.X, -v.Y);
	public static V operator +(V v1, V v2) => (v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => (v1.X - v2.X, v1.Y - v2.Y);
	public static V operator *(double c, V v) => v * c;
	public static V operator *(V v, double c) => (v.X * c, v.Y * c);
	public static V operator /(V v, double c) => (v.X / c, v.Y / c);

	public double NormL1 => Math.Abs(X) + Math.Abs(Y);
	public double Norm => Math.Sqrt(X * X + Y * Y);
	public double Angle => Math.Atan2(Y, X);
	public double Cos => X / Norm;
	public double Sin => Y / Norm;
	public double Tan => Y / X;

	public V Normalize() => this / Norm;
	public V Rotate(double angle)
	{
		var cos = Math.Cos(angle);
		var sin = Math.Sin(angle);
		return (cos * X - sin * Y, sin * X + cos * Y);
	}

	public static double Dot(V v1, V v2) => v1.X * v2.X + v1.Y * v2.Y;
	// 菱形の面積
	public static double Area(V v1, V v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y);
	public static bool IsParallel(V v1, V v2) => v1.X * v2.Y == v2.X * v1.Y;
	public static bool IsOrthogonal(V v1, V v2) => Dot(v1, v2) == 0;

	// 分点
	public static V Divide(V v1, V v2, double m, double n) => (n * v1 + m * v2) / (m + n);
}
