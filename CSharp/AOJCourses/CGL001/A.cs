﻿using System;
using System.Linq;

class A
{
	static double[] Read() => Console.ReadLine().Split().Select(double.Parse).ToArray();
	static void Main()
	{
		var h = Read();

		// Vector2 は float のため、精度が足りません。
		var p1 = new V(h[0], h[1]);
		var p2 = new V(h[2], h[3]);
		var p0 = p2 - p1;
		var e = p0 / p0.Norm;

		var q = int.Parse(Console.ReadLine());
		var r = new int[q].Select(_ => Read())
			.Select(x => new V(x[0], x[1]) - p1)
			.Select(p => p1 + V.Dot(p, e) * e);
		Console.WriteLine(string.Join("\n", r));
	}
}

struct V
{
	public double X, Y;
	public V(double x, double y) { X = x; Y = y; }

	public static V operator +(V v) => v;
	public static V operator -(V v) => new V(-v.X, -v.Y);
	public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
	public static V operator *(double c, V v) => new V(v.X * c, v.Y * c);
	public static V operator *(V v, double c) => new V(v.X * c, v.Y * c);
	public static V operator /(V v, double c) => new V(v.X / c, v.Y / c);

	public static bool operator ==(V v1, V v2) => v1.X == v2.X && v1.Y == v2.Y;
	public static bool operator !=(V v1, V v2) => !(v1 == v2);
	public override bool Equals(object obj) => obj is V && this == (V)obj;
	public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
	public override string ToString() => $"{X:F9} {Y:F9}";

	public double Norm => Math.Sqrt(X * X + Y * Y);
	public static double Dot(V v1, V v2) => v1.X * v2.X + v1.Y * v2.Y;
}
