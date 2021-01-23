using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var b = ReadL();

		var c = Convolution(a, b);
		Console.WriteLine(string.Join(" ", c.Take(n + m - 1)));
	}

	// n: Power of 2
	// { f(z^i) }
	public static ComplexD[] Fft(ComplexD[] c, bool inverse = false)
	{
		var n = c.Length;
		if (n == 1) return c;

		var n2 = n / 2;
		var c1 = new ComplexD[n2];
		var c2 = new ComplexD[n2];
		for (int i = 0; i < n2; ++i)
		{
			c1[i] = c[2 * i];
			c2[i] = c[2 * i + 1];
		}

		var f1 = Fft(c1, inverse);
		var f2 = Fft(c2, inverse);

		var r = new ComplexD[n];
		for (int i = 0; i < n2; ++i)
		{
			var z = f2[i] * ComplexD.NthRoot(n, inverse ? -i : i);
			r[i] = f1[i] + z;
			r[n2 + i] = f1[i] - z;
			if (inverse)
			{
				r[i] /= 2;
				r[n2 + i] /= 2;
			}
		}
		return r;
	}

	// n: Power of 2
	static ComplexD[] Convolution(ComplexD[] a, ComplexD[] b)
	{
		var fa = Fft(a);
		var fb = Fft(b);
		for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
		return Fft(fa, true);
	}

	const long M = 998244353;
	public static decimal[] Convolution(long[] a, long[] b)
	{
		var n = 1;
		while (n <= a.Length + b.Length - 2) n *= 2;

		var ac = new ComplexD[n];
		var bc = new ComplexD[n];
		for (int i = 0; i < a.Length; ++i) ac[i] = a[i];
		for (int i = 0; i < b.Length; ++i) bc[i] = b[i];

		return Array.ConvertAll(Convolution(ac, bc), c => Math.Round(c.X) % M);
	}
}

struct ComplexD
{
	public decimal X, Y;
	public ComplexD(decimal x, decimal y) => (X, Y) = (x, y);
	public void Deconstruct(out decimal x, out decimal y) => (x, y) = (X, Y);
	public override string ToString() => $"{X:F9} {Y:F9}";

	public static implicit operator ComplexD(int v) => new ComplexD(v, 0);
	public static implicit operator ComplexD(long v) => new ComplexD(v, 0);
	public static implicit operator ComplexD(decimal v) => new ComplexD(v, 0);

	public static ComplexD operator -(ComplexD v) => new ComplexD(-v.X, -v.Y);
	public static ComplexD operator +(ComplexD v1, ComplexD v2) => new ComplexD(v1.X + v2.X, v1.Y + v2.Y);
	public static ComplexD operator -(ComplexD v1, ComplexD v2) => new ComplexD(v1.X - v2.X, v1.Y - v2.Y);
	public static ComplexD operator *(ComplexD v1, ComplexD v2) => new ComplexD(v1.X * v2.X - v1.Y * v2.Y, v1.X * v2.Y + v1.Y * v2.X);
	public static ComplexD operator *(decimal c, ComplexD v) => v * c;
	public static ComplexD operator *(ComplexD v, decimal c) => new ComplexD(v.X * c, v.Y * c);
	public static ComplexD operator /(ComplexD v, decimal c) => new ComplexD(v.X / c, v.Y / c);

	public static ComplexD NthRoot(int n, int i)
	{
		var a = i * 2 * Math.PI / n;
		return new ComplexD((decimal)Math.Cos(a), (decimal)Math.Sin(a));
	}

	public double Angle => Math.Atan2((double)Y, (double)X);
	public decimal Tan => Y / X;

	public ComplexD Rotate(double angle)
	{
		var cos = (decimal)Math.Cos(angle);
		var sin = (decimal)Math.Sin(angle);
		return new ComplexD(cos * X - sin * Y, sin * X + cos * Y);
	}
}
