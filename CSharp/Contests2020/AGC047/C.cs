using System;
using System.Numerics;

class C
{
	const long p = 200003, g = 2;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		// g^{p-1} == 1
		var pg = new long[p - 1];
		var pg_ = new long[p];
		pg[0] = 1;

		for (int i = 1; i < pg.Length; ++i)
		{
			pg[i] = pg[i - 1] * g % p;
			pg_[pg[i]] = i;
		}

		var c = new long[p - 1];
		var sq = 0L;
		foreach (var x in a)
		{
			if (x == 0) continue;
			c[pg_[x]]++;
			sq += pg[2 * pg_[x] % (p - 1)];
		}
		var conv = Dft.Convolution(c, c);

		var r = 0L;
		for (int d = 0; d < conv.Length; d++)
		{
			if (conv[d] == 0) continue;
			r += conv[d] * pg[d % (p - 1)];
		}
		Console.WriteLine((r - sq) / 2);
	}
}

public class Dft
{
	public static long[] ToLong(Complex[] a) => Array.ConvertAll(a, c => (long)Math.Round(c.Real));
	public static Complex[] ToComplex(long[] a) => Array.ConvertAll(a, c => new Complex(c, 0));

	public static int ToPowerOf2(int length)
	{
		var n = 1;
		while (n < length) n <<= 1;
		return n;
	}

	static Complex[] NthRoots(int n)
	{
		var r = new Complex[n + 1];
		for (int i = 0; i <= n; ++i) r[i] = Complex.Exp(new Complex(0, i * 2 * Math.PI / n));
		return r;
	}

	int n;
	Complex[] roots;
	public Dft(int length)
	{
		n = ToPowerOf2(length);
		roots = NthRoots(n);
	}

	void FftInternal(Complex[] c, bool inverse)
	{
		var m = c.Length;
		if (m == 1) return;

		var m2 = m / 2;
		var nm = n / m;
		var c1 = new Complex[m2];
		var c2 = new Complex[m2];
		for (int i = 0; i < m2; ++i)
		{
			c1[i] = c[2 * i];
			c2[i] = c[2 * i + 1];
		}

		FftInternal(c1, inverse);
		FftInternal(c2, inverse);

		for (int i = 0; i < m2; ++i)
		{
			var z = c2[i] * roots[nm * (inverse ? m - i : i)];
			c[i] = c1[i] + z;
			c[m2 + i] = c1[i] - z;
		}
	}

	// { f(w^i) }
	// 長さは n 以下で OK。
	public Complex[] Fft(Complex[] c, bool inverse = false)
	{
		var r = new Complex[n];
		c.CopyTo(r, 0);
		FftInternal(r, inverse);
		if (inverse) for (int i = 0; i < n; ++i) r[i] /= n;
		return r;
	}

	// 長さは n 以下で OK。
	public static Complex[] Convolution(Complex[] a, Complex[] b)
	{
		var dft = new Dft(a.Length + b.Length - 1);
		var fa = dft.Fft(a);
		var fb = dft.Fft(b);
		for (int i = 0; i < dft.n; ++i) fa[i] *= fb[i];
		return dft.Fft(fa, true);
	}

	public static long[] Convolution(long[] a, long[] b)
	{
		return ToLong(Convolution(ToComplex(a), ToComplex(b)));
	}
}
