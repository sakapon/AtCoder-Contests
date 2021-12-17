using System;
using System.Numerics;
using System.Text;

class F
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

		var ab = FFT.Convolution(a, b);
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
