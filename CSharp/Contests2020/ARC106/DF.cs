using System;

class DF
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, k) = Read2();
		var a = ReadL();

		var p2 = new long[k + 1];
		var f = new long[k + 1];
		var f_ = new long[k + 1];
		p2[0] = f[0] = f_[0] = 1;
		for (int x = 1; x <= k; x++)
		{
			p2[x] = p2[x - 1] * 2 % M;
			f[x] = f[x - 1] * x % M;
			f_[x] = f_[x - 1] * MInv(x) % M;
		}

		var pa = Array.ConvertAll(a, _ => 1L);
		var s1 = new long[k + 1];
		var s2 = new long[k + 1];
		s1[0] = n;

		for (int x = 1; x <= k; x++)
		{
			var sum = 0L;
			for (int i = 0; i < n; i++)
				sum += pa[i] = pa[i] * a[i] % M;
			sum %= M;

			s1[x] = sum * f_[x] % M;
			s2[x] = sum * p2[x] % M;
		}

		var conv = Ntt.Convolution(s1, s1);

		var half = MInv(2);
		for (int x = 1; x <= k; x++)
			Console.WriteLine(MInt((f[x] * conv[x] % M - s2[x]) * half));
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
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
