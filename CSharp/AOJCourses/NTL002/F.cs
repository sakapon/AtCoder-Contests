using System;
using System.Numerics;

class F
{
	static void Main()
	{
		var h = Console.ReadLine().Split();
		char[] sa = h[0].ToCharArray(), sb = h[1].ToCharArray();

		var neg_a = sa[0] == '-';
		var neg_b = sb[0] == '-';
		var neg_ab = neg_a ^ neg_b;

		var a = Array.ConvertAll(sa, c => c - '0');
		var b = Array.ConvertAll(sb, c => c - '0');
		Array.Reverse(a);
		Array.Reverse(b);
		if (neg_a) Array.Resize(ref a, a.Length - 1);
		if (neg_b) Array.Resize(ref b, b.Length - 1);

		var ab = Convolution(a, b);
		for (int i = 0; i < ab.Length - 1; i++)
		{
			ab[i + 1] += ab[i] / 10;
			ab[i] %= 10;
		}

		Array.Reverse(ab);
		var sab = string.Join("", ab);
		sab = sab.TrimStart('0');
		if (neg_ab && sab != "") sab = "-" + sab;
		if (sab == "") sab = "0";
		Console.WriteLine(sab);
	}

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

	// n: Power of 2
	static Complex[] Convolution(Complex[] a, Complex[] b)
	{
		var fa = Fft(a);
		var fb = Fft(b);
		for (int i = 0; i < a.Length; ++i) fa[i] *= fb[i];
		return Fft(fa, true);
	}

	public static int[] Convolution(int[] a, int[] b)
	{
		var n = 1;
		while (n <= a.Length + b.Length - 2) n *= 2;

		var ac = new Complex[n];
		var bc = new Complex[n];
		for (int i = 0; i < a.Length; ++i) ac[i] = a[i];
		for (int i = 0; i < b.Length; ++i) bc[i] = b[i];

		return Array.ConvertAll(Convolution(ac, bc), c => (int)Math.Round(c.Real));
	}
}
