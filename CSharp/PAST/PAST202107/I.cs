using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		V p1 = Read2();
		V p2 = Read2();
		var abs = Array.ConvertAll(new bool[n], _ => (V)Read2());

		var d = (p1 + p2) / 2;
		abs = Array.ConvertAll(abs, p => p - d);

		var angle = (p2 - p1).Angle;
		abs = Array.ConvertAll(abs, p => p.Rotate(-angle));

		return string.Join("\n", abs);
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
