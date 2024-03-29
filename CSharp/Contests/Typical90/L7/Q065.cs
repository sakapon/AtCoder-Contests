﻿using System;

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

		rs = FNTT.Convolution(rs, gs);
		rs = FNTT.Convolution(rs, bs);
		return rs[K];
	}

	// MCombination の M を変更します。
}

public class FNTT
{
	const long p = 998244353, g = 3;
	//const long p = 1107296257, g = 10;

	public static int ToPowerOf2(int n)
	{
		var p = 1;
		while (p < n) p <<= 1;
		return p;
	}

	// コピー先のインデックス O(n)
	// n = 8: { 0, 4, 2, 6, 1, 5, 3, 7 }
	static int[] BitReversal(int n)
	{
		var b = new int[n];
		for (int p = 1, d = n >> 1; p < n; p <<= 1, d >>= 1)
			for (int k = 0; k < p; ++k)
				b[k | p] = b[k] | d;
		return b;
	}

	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
		return r;
	}

	// k 番目の 1 の n 乗根 (0 <= k < n/2)
	static long[] NthRoots(int n, long w)
	{
		var r = new long[n >> 1];
		if (r.Length > 0) r[0] = 1;
		for (int k = 1; k < r.Length; ++k)
			r[k] = r[k - 1] * w % p;
		return r;
	}

	int n;
	public int Length => n;
	long nInv;
	int[] br;
	long[] roots;

	// length は 2 の冪に変更されます。
	public FNTT(int length)
	{
		n = ToPowerOf2(length);
		nInv = MPow(n, p - 2);
		br = BitReversal(n);
		roots = NthRoots(n, MPow(g, (p - 1) / n));
	}

	// c の長さは 2 の冪とします。
	// h: 更新対象の長さの半分
	void TransformRecursive(long[] c, int l, int h)
	{
		if (h == 0) return;
		var d = (n >> 1) / h;

		TransformRecursive(c, l, h >> 1);
		TransformRecursive(c, l + h, h >> 1);

		for (int k = 0; k < h; ++k)
		{
			var v0 = c[l + k];
			var v1 = c[l + k + h] * roots[d * k] % p;
			c[l + k] = (v0 + v1) % p;
			c[l + k + h] = (v0 - v1 + p) % p;
		}
	}

	public long[] Transform(long[] c, bool inverse)
	{
		if (c == null) throw new ArgumentNullException(nameof(c));

		var t = new long[n];
		for (int k = 0; k < c.Length; ++k)
			t[br[k]] = c[k];

		TransformRecursive(t, 0, n >> 1);

		if (inverse && n > 1)
		{
			Array.Reverse(t, 1, n - 1);
			for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % p;
		}
		return t;
	}

	// 戻り値の長さは |a| + |b| - 1 となります。
	public static long[] Convolution(long[] a, long[] b)
	{
		if (a == null) throw new ArgumentNullException(nameof(a));
		if (b == null) throw new ArgumentNullException(nameof(b));

		var n = a.Length + b.Length - 1;
		var ntt = new FNTT(n);

		var fa = ntt.Transform(a, false);
		var fb = ntt.Transform(b, false);

		for (int k = 0; k < fa.Length; ++k)
		{
			fa[k] = fa[k] * fb[k] % p;
		}
		var c = ntt.Transform(fa, true);

		if (n < c.Length) Array.Resize(ref c, n);
		return c;
	}
}
