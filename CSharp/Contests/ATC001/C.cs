using System;
using System.Linq;
using System.Numerics;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var n2 = 1;
		while (n2 <= 2 * n) n2 *= 2;

		var a = new Complex[n2];
		var b = new Complex[n2];
		for (int i = 1; i <= n; i++)
		{
			var x = Read();
			a[i] = x[0];
			b[i] = x[1];
		}

		var fa = Fft(a);
		var fb = Fft(b);
		var fab = fa.Zip(fb, (x, y) => x * y).ToArray();
		var ab = Fft(fab, true);

		Console.WriteLine(string.Join("\n", ab.Skip(1).Take(2 * n).Select(c => $"{c.Real:F0}")));
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
			var z = Complex.Exp(new Complex(0, (inverse ? i : -i) * 2 * Math.PI / n));
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
}
