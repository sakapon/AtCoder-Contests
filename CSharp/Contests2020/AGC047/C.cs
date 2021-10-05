using System;
using System.Numerics;

class C
{
	const long p = 200003, g = 2;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		// g^{p-1} == 1
		var pg = new long[p - 1];
		var pg_ = new long[p];
		pg[0] = 1;

		for (int i = 1; i < pg.Length; ++i)
		{
			pg[i] = pg[i - 1] * g % p;
			pg_[pg[i]] = i;
		}

		var c = new long[p - 1];
		var sq = 0L;
		foreach (var x in a)
		{
			if (x == 0) continue;
			c[pg_[x]]++;
			sq += pg[2 * pg_[x] % (p - 1)];
		}
		var conv = FFT.Convolution(c, c);

		var r = 0L;
		for (int d = 0; d < conv.Length; d++)
		{
			if (conv[d] == 0) continue;
			r += conv[d] * pg[d % (p - 1)];
		}
		Console.WriteLine((r - sq) / 2);
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
