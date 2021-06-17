using System;

class Q065
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (R, G, B, K) = Read4();
		var (X, Y, Z) = Read3();

		var mc = new MCombination(R + G + B);
		var rs = new long[R + 1];
		var gs = new long[G + 1];
		var bs = new long[B + 1];

		for (int r = K - Y; r <= R; r++)
			rs[r] = mc.MNcr(R, r);
		for (int g = K - Z; g <= G; g++)
			gs[g] = mc.MNcr(G, g);
		for (int b = K - X; b <= B; b++)
			bs[b] = mc.MNcr(B, b);

		rs = Ntt.Convolution(rs, gs);
		rs = Ntt.Convolution(rs, bs);
		return rs[K];
	}

	// MCombination の M を変更します。
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
