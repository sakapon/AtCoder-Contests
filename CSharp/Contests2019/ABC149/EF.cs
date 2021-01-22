using System;
using System.Linq;
using System.Numerics;

class EF
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		long n = h[0], m = h[1];
		var a = ReadL();

		var c = Tally(a, a.Max());
		var conv = Dft.Convolution(c, c);

		var r = 0L;
		var count = 0L;

		for (int x = conv.Length - 1; x >= 0 && count < m; x--)
		{
			if (conv[x] == 0) continue;
			var nc = Math.Min(m, count + conv[x]);
			r += x * (nc - count);
			count = nc;
		}
		Console.WriteLine(r);
	}

	static long[] Tally(long[] a, long max)
	{
		var c = new long[max + 1];
		foreach (var x in a) ++c[x];
		return c;
	}
}

static class Dft
{
	static Complex NthRoot(int n, int i)
	{
		var t = i * 2 * Math.PI / n;
		return new Complex(Math.Cos(t), Math.Sin(t));
	}

	// n: Power of 2
	// { f(z^i) }
	public static Complex[] Fft(Complex[] c, bool inverse = false)
	{
		var n = c.Length;
		if (n == 1) return c;
		var n2 = n / 2;
		var c1 = new Complex[n2];
		var c2 = new Complex[n2];

		for (int i = 0; i < n2; ++i)
		{
			c1[i] = c[2 * i];
			c2[i] = c[2 * i + 1];
		}

		var f1 = Fft(c1, inverse);
		var f2 = Fft(c2, inverse);

		var r = new Complex[n];
		for (int i = 0; i < n2; ++i)
		{
			var z = NthRoot(n, inverse ? -i : i);
			r[i] = f1[i] + z * f2[i];
			r[n2 + i] = f1[i] - z * f2[i];
			if (inverse)
			{
				r[i] /= 2;
				r[n2 + i] /= 2;
			}
		}
		return r;
	}

	// n: Power of 2
	static Complex[] Convolution(Complex[] a, Complex[] b)
	{
		var fa = Fft(a);
		var fb = Fft(b);
		for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
		return Fft(fa, true);
	}

	public static long[] Convolution(long[] a, long[] b)
	{
		var n = 1;
		while (n <= a.Length + b.Length - 2) n *= 2;

		var ac = new Complex[n];
		var bc = new Complex[n];
		for (int i = 0; i < a.Length; ++i) ac[i] = a[i];
		for (int i = 0; i < b.Length; ++i) bc[i] = b[i];

		return Array.ConvertAll(Convolution(ac, bc), c => (long)Math.Round(c.Real));
	}
}
