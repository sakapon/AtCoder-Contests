using System;
using System.Collections.Generic;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new V();
		var g = new V(100, 0);
		var r = Koch(s, g, n);
		r.Insert(0, s);
		r.Add(g);
		Console.WriteLine(string.Join("\n", r));
	}

	// 端点を含みません。
	static List<V> Koch(V s, V g, int n)
	{
		var r = new List<V>();
		if (n-- == 0) return r;

		var v1 = V.Divide(s, g, 1, 2);
		var v3 = V.Divide(s, g, 2, 1);
		var sg = g - s;
		var v2 = V.Divide(s, g, 1, 1) + sg.NormL2 / (2 * Math.Sqrt(3)) * new V(-sg.Y, sg.X).Normalize();

		r.AddRange(Koch(s, v1, n));
		r.Add(v1);
		r.AddRange(Koch(v1, v2, n));
		r.Add(v2);
		r.AddRange(Koch(v2, v3, n));
		r.Add(v3);
		r.AddRange(Koch(v3, g, n));
		return r;
	}
}

struct V
{
	public double X, Y;
	public V(double x, double y) { X = x; Y = y; }
	public override string ToString() => $"{X:F9} {Y:F9}";

	public static V operator -(V v) => new V(-v.X, -v.Y);
	public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
	public static V operator *(double c, V v) => v * c;
	public static V operator *(V v, double c) => new V(v.X * c, v.Y * c);
	public static V operator /(V v, double c) => new V(v.X / c, v.Y / c);

	public double NormL2 => Math.Sqrt(X * X + Y * Y);
	public V Normalize() => this / NormL2;
	public static V Divide(V v1, V v2, double m, double n) => (n * v1 + m * v2) / (m + n);
}
