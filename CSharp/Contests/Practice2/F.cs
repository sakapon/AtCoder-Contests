﻿using System;

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

		var c = Ntt.Convolution(a, b);
		Console.WriteLine(string.Join(" ", c[..(n + m - 1)]));
	}
}

public class Ntt
{
	const long p = 998244353, g = 3;

	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
		return r;
	}

	static long[] NthRoots(int n)
	{
		var w = MPow(g, (p - 1) / n);
		var r = new long[n + 1];
		r[0] = 1;
		for (int i = 0; i < n; ++i) r[i + 1] = r[i] * w % p;
		return r;
	}

	// n: Power of 2
	int n;
	long nInv;
	long[] roots;
	public Ntt(int n)
	{
		this.n = n;
		nInv = MPow(n, p - 2);
		roots = NthRoots(n);
	}

	void FftInternal(long[] c, bool inverse)
	{
		var m = c.Length;
		if (m == 1) return;

		var m2 = m / 2;
		var nm = n / m;
		var c1 = new long[m2];
		var c2 = new long[m2];
		for (int i = 0; i < m2; ++i)
		{
			c1[i] = c[2 * i];
			c2[i] = c[2 * i + 1];
		}

		FftInternal(c1, inverse);
		FftInternal(c2, inverse);

		for (int i = 0; i < m2; ++i)
		{
			var z = c2[i] * roots[nm * (inverse ? m - i : i)] % p;
			c[i] = (c1[i] + z) % p;
			c[m2 + i] = (c1[i] - z + p) % p;
		}
	}

	// { f(ω^i) }
	public void Fft(long[] c, bool inverse = false)
	{
		FftInternal(c, inverse);
		if (inverse) for (int i = 0; i < n; ++i) c[i] = c[i] * nInv % p;
	}

	// n: Power of 2
	long[] ConvolutionInternal(long[] a, long[] b)
	{
		Fft(a);
		Fft(b);
		for (int i = 0; i < n; ++i) a[i] = a[i] * b[i] % p;
		Fft(a, true);
		return a;
	}

	public static long[] Convolution(long[] a, long[] b)
	{
		var n = 1;
		while (n <= a.Length + b.Length - 2) n *= 2;

		var ac = new long[n];
		var bc = new long[n];
		a.CopyTo(ac, 0);
		b.CopyTo(bc, 0);
		return new Ntt(n).ConvolutionInternal(ac, bc);
	}
}
