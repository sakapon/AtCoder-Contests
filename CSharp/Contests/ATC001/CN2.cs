using System;
using System.Linq;
using System.Numerics;

class CN2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new long[n + 1];
		var b = new long[n + 1];
		for (int i = 1; i <= n; i++)
		{
			var v = Read();
			a[i] = v[0];
			b[i] = v[1];
		}

		var ab1 = new FMT302(1 << 20, p1, g1).Convolution(a, b);
		var ab2 = new FMT302(1 << 20, p2, g2).Convolution(a, b);

		var ab = ab1.Zip(ab2, (x, y) => Crt(p1, p2, x, y)).ToArray();
		Console.WriteLine(string.Join("\n", ab[1..]));
	}

	const long p1 = 7340033, g1 = 3;
	const long p2 = 13631489, g2 = 15;

	static long[] ExtendedEuclid(long a, long b)
	{
		if (b == 1) return new[] { 1, 1 - a };
		long r;
		var q = Math.DivRem(a, b, out r);
		var t = ExtendedEuclid(b, r);
		return new[] { t[1], t[0] - q * t[1] };
	}

	// a mod m かつ b mod n である値 (mod mn で唯一)
	// 前提: m と n は互いに素。
	static long Crt(long m, long n, long a, long b)
	{
		var v = ExtendedEuclid(m, n);
		var r = (BigInteger)a * n * v[1] + (BigInteger)b * m * v[0];
		return (long)((r %= m * n) < 0 ? r + m * n : r);
	}
}

public class FMT302
{
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

	long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
		return r;
	}

	// k 番目の 1 の n 乗根 (0 <= k < n/2)
	long[] NthRoots(int n, long w)
	{
		var r = new long[n >> 1];
		r[0] = 1;
		for (int k = 1; k < r.Length; ++k)
			r[k] = r[k - 1] * w % p;
		return r;
	}

	public int MaxLength { get; }
	long p;
	int[] br;
	long[] roots;

	// maxLength は 2 の冪に変更されます。
	public FMT302(int maxLength = 1 << 20, long p = 998244353, long g = 3)
	{
		MaxLength = ToPowerOf2(maxLength);
		this.p = p;
		br = BitReversal(MaxLength);
		roots = NthRoots(MaxLength, MPow(g, (p - 1) / MaxLength));
	}

	// c の長さは 2 の冪とします。
	// h: 更新対象の長さの半分
	void TransformRecursive(long[] c, int l, int h)
	{
		if (h == 0) return;
		var d = (MaxLength >> 1) / h;

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

	// 戻り値の長さは 2 の冪となります。
	public long[] Transform(long[] c, bool inverse, int resultLength = -1)
	{
		if (c == null) throw new ArgumentNullException(nameof(c));

		var n = ToPowerOf2(resultLength == -1 ? c.Length : resultLength);
		var d = MaxLength / n;

		var t = new long[n];
		for (int k = 0; k < c.Length; ++k)
			t[br[d * k]] = c[k];

		TransformRecursive(t, 0, n >> 1);

		if (inverse && n > 1)
		{
			Array.Reverse(t, 1, n - 1);
			var nInv = MPow(n, p - 2);
			for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % p;
		}
		return t;
	}

	// 戻り値の長さは |a| + |b| - 1 となります。
	public long[] Convolution(long[] a, long[] b)
	{
		if (a == null) throw new ArgumentNullException(nameof(a));
		if (b == null) throw new ArgumentNullException(nameof(b));

		var n = a.Length + b.Length - 1;

		var fa = Transform(a, false, n);
		var fb = Transform(b, false, n);

		for (int k = 0; k < fa.Length; ++k)
		{
			fa[k] = fa[k] * fb[k] % p;
		}
		var c = Transform(fa, true);

		if (n < c.Length) Array.Resize(ref c, n);
		return c;
	}
}
