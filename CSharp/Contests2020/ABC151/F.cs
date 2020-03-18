using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).Select(x => new V(x[0], x[1])).ToArray();

		if (n == 2) { Console.WriteLine((p[1] - p[0]).Norm / 2); return; }

		var M = 0.0;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int k = j + 1; k < n; k++)
					M = Math.Max(M, GetRadius(p[i], p[j], p[k]));
		Console.WriteLine(M);
	}

	static double GetRadius(V p0, V p1, V p2)
	{
		var v1 = p1 - p0;
		var v2 = p2 - p0;
		var v3 = p2 - p1;
		var a = v1.Norm;
		var b = v2.Norm;
		var c = v3.Norm;

		var s_4 = Math.Abs(v1.X * v2.Y - v2.X * v1.Y) * 2;
		if (s_4 == 0) return Math.Max(Math.Max(a, b), c) / 2;
		if (Cos(a, b, c) <= 0) return a / 2;
		if (Cos(b, c, a) <= 0) return b / 2;
		if (Cos(c, a, b) <= 0) return c / 2;
		return a * b * c / s_4;
	}

	static double Cos(double a, double b, double c)
	{
		return (b * b + c * c - a * a) / (2 * b * c);
	}
}

struct V
{
	public static V Zero = new V();
	public static V XBasis = new V(1, 0);
	public static V YBasis = new V(0, 1);

	public int X, Y;
	public V(int x, int y) { X = x; Y = y; }

	public double Norm => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

	public static V operator +(V v) => v;
	public static V operator -(V v) => new V(-v.X, -v.Y);
	public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);

	public static bool operator ==(V v1, V v2) => v1.X == v2.X && v1.Y == v2.Y;
	public static bool operator !=(V v1, V v2) => !(v1 == v2);
	public override bool Equals(object obj) => obj is V && this == (V)obj;
	public override int GetHashCode() => X ^ Y;
	public override string ToString() => $"{X} {Y}";
}
