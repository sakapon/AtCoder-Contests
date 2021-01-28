using System;
using System.Text;

class FN
{
	static void Main()
	{
		var h = Console.ReadLine().Split();
		char[] sa = h[0].ToCharArray(), sb = h[1].ToCharArray();

		var neg_a = sa[0] == '-';
		var neg_b = sb[0] == '-';
		var neg_ab = neg_a ^ neg_b;

		var a = Array.ConvertAll(sa, c => (long)(c - '0'));
		var b = Array.ConvertAll(sb, c => (long)(c - '0'));
		Array.Reverse(a);
		Array.Reverse(b);
		if (neg_a) Array.Resize(ref a, a.Length - 1);
		if (neg_b) Array.Resize(ref b, b.Length - 1);

		var ab = Ntt.Convolution(a, b);
		for (int i = 0; i < ab.Length - 1; i++)
		{
			if (ab[i] < 10) continue;
			ab[i + 1] += ab[i] / 10;
			ab[i] %= 10;
		}

		Console.WriteLine(ToString(ab, neg_ab));
	}

	static string ToString(long[] c, bool neg)
	{
		var i = c.Length - 1;
		while (i >= 0 && c[i] == 0) i--;
		if (i == -1) return "0";

		var sb = new StringBuilder();
		if (neg) sb.Append('-');
		while (i >= 0) sb.Append(c[i--]);
		return sb.ToString();
	}
}

public class Ntt
{
	const long p = 998244353, g = 3;

	public static int ToPowerOf2(int length)
	{
		var n = 1;
		while (n < length) n <<= 1;
		return n;
	}

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

	int n;
	long nInv;
	long[] roots;
	public Ntt(int length)
	{
		n = ToPowerOf2(length);
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

	// { f(w^i) }
	// 長さは n 以下で OK。
	public long[] Fft(long[] c, bool inverse = false)
	{
		var r = new long[n];
		c.CopyTo(r, 0);
		FftInternal(r, inverse);
		if (inverse) for (int i = 0; i < n; ++i) r[i] = r[i] * nInv % p;
		return r;
	}

	// 長さは n 以下で OK。
	public static long[] Convolution(long[] a, long[] b)
	{
		var ntt = new Ntt(a.Length + b.Length - 1);
		var fa = ntt.Fft(a);
		var fb = ntt.Fft(b);
		for (int i = 0; i < ntt.n; ++i) fa[i] = fa[i] * fb[i] % p;
		return ntt.Fft(fa, true);
	}
}
