﻿using System;
using System.Linq;
using System.Numerics;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => (long)(c - '0')).ToArray();
		var t = Console.ReadLine().Select(c => (long)(c - '0')).ToArray();

		var n = s.Length;
		var m = t.Length;

		var rsq = new StaticRSQ1(s);
		var tSum = t.Sum();

		var tr = t.Reverse().ToArray();
		var conv = FFT.Convolution(s, tr);

		return Enumerable.Range(0, n - m + 1).Min(XorSum);

		long XorSum(int j)
		{
			var r = tSum;
			r += rsq.GetSum(j, j + m);
			r -= 2 * conv[j + m - 1];
			return r;
		}
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}

public class FFT
{
	public static long[] ToInt64(Complex[] a) => Array.ConvertAll(a, x => (long)Math.Round(x.Real));
	public static Complex[] ToComplex(long[] a) => Array.ConvertAll(a, x => new Complex(x, 0));

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

	// k 番目の 1 の n 乗根 (0 <= k < n/2)
	static Complex[] NthRoots(int n)
	{
		var r = new Complex[n >> 1];
		for (int k = 0; k < r.Length; ++k)
			r[k] = Complex.FromPolarCoordinates(1, 2 * Math.PI * k / n);
		return r;
	}

	int n;
	public int Length => n;
	int[] br;
	Complex[] roots;

	// length は 2 の冪に変更されます。
	public FFT(int length)
	{
		n = ToPowerOf2(length);
		br = BitReversal(n);
		roots = NthRoots(n);
	}

	// c の長さは 2 の冪とします。
	// h: 更新対象の長さの半分
	void TransformRecursive(Complex[] c, int l, int h)
	{
		if (h == 0) return;
		var d = (n >> 1) / h;

		TransformRecursive(c, l, h >> 1);
		TransformRecursive(c, l + h, h >> 1);

		for (int k = 0; k < h; ++k)
		{
			var v0 = c[l + k];
			var v1 = c[l + k + h] * roots[d * k];
			c[l + k] = v0 + v1;
			c[l + k + h] = v0 - v1;
		}
	}

	// f が整数でも f^ は整数になるとは限りません。
	public Complex[] Transform(Complex[] c, bool inverse)
	{
		if (c == null) throw new ArgumentNullException(nameof(c));

		var t = new Complex[n];
		for (int k = 0; k < c.Length; ++k)
			t[br[k]] = c[k];

		TransformRecursive(t, 0, n >> 1);

		if (inverse && n > 1)
		{
			Array.Reverse(t, 1, n - 1);
			for (int k = 0; k < n; ++k) t[k] /= n;
		}
		return t;
	}

	// 戻り値の長さは |a| + |b| - 1 となります。
	public static Complex[] Convolution(Complex[] a, Complex[] b)
	{
		if (a == null) throw new ArgumentNullException(nameof(a));
		if (b == null) throw new ArgumentNullException(nameof(b));

		var n = a.Length + b.Length - 1;
		var fft = new FFT(n);

		var fa = fft.Transform(a, false);
		var fb = fft.Transform(b, false);

		for (int k = 0; k < fa.Length; ++k)
		{
			fa[k] *= fb[k];
		}
		var c = fft.Transform(fa, true);

		if (n < c.Length) Array.Resize(ref c, n);
		return c;
	}

	public static long[] Convolution(long[] a, long[] b)
	{
		if (a == null) throw new ArgumentNullException(nameof(a));
		if (b == null) throw new ArgumentNullException(nameof(b));
		return ToInt64(Convolution(ToComplex(a), ToComplex(b)));
	}
}
