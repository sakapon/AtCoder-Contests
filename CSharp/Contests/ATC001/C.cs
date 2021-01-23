using System;
using System.Linq;
using System.Numerics;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new long[n + 1];
		var b = new long[n + 1];
		for (int i = 1; i <= n; i++)
		{
			var v = Read();
			a[i] = v[0];
			b[i] = v[1];
		}

		var ab = Convolution(a, b);
		Console.WriteLine(string.Join("\n", ab.Skip(1).Take(2 * n)));
	}

	static Complex NthRoot(int n, int i)
	{
		var t = i * 2 * Math.PI / n;
		return new Complex(Math.Cos(t), Math.Sin(t));
	}

	static Complex[] Fft(Complex[] c, bool inverse = false)
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
			var z = f2[i] * NthRoot(n, inverse ? -i : i);
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

	static Complex[] Convolution(Complex[] a, Complex[] b)
	{
		var fa = Fft(a);
		var fb = Fft(b);
		for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
		return Fft(fa, true);
	}

	static long[] Convolution(long[] a, long[] b)
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
