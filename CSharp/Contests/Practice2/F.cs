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

	const long p = 998244353;
	const long g = 3;
	const long half = (p + 1) / 2;

	static long MInt(long x, long M) => (x %= M) < 0 ? x + M : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
		return r;
	}

	static long NthRoot(int n, int i)
	{
		return MPow(g, (p - 1) / n * MInt(i, n));
	}

	// n: Power of 2
	// { f(z^i) }
	public static long[] Fft(long[] c, bool inverse = false)
	{
		var n = c.Length;
		if (n == 1) return c;

		var n2 = n / 2;
		var c1 = new long[n2];
		var c2 = new long[n2];
		for (int i = 0; i < n2; ++i)
		{
			c1[i] = c[2 * i];
			c2[i] = c[2 * i + 1];
		}

		var f1 = Fft(c1, inverse);
		var f2 = Fft(c2, inverse);

		var r = new long[n];
		for (int i = 0; i < n2; ++i)
		{
			var z = f2[i] * NthRoot(n, inverse ? -i : i) % p;
			r[i] = (f1[i] + z) % p;
			r[n2 + i] = (f1[i] - z + p) % p;
			if (inverse)
			{
				r[i] = r[i] * half % p;
				r[n2 + i] = r[n2 + i] * half % p;
			}
		}
		return r;
	}

	// n: Power of 2
	static long[] Convolution_In(long[] a, long[] b)
	{
		var fa = Fft(a);
		var fb = Fft(b);
		for (int i = 0; i < a.Length; ++i) fa[i] = fa[i] * fb[i] % p;
		return Fft(fa, true);
	}

	public static long[] Convolution(long[] a, long[] b)
	{
		var n = 1;
		while (n <= a.Length + b.Length - 2) n *= 2;

		var ac = new long[n];
		var bc = new long[n];
		a.CopyTo(ac, 0);
		b.CopyTo(bc, 0);

		return Convolution_In(ac, bc);
	}
}
