using System;

namespace CoderLib8.Numerics
{
	// From: https://kazuma8128.hatenablog.com/entry/2018/05/31/144519
	// Test: https://yukicoder.me/problems/no/1304
	public static class Hadamard
	{
		const long M = 998244353;
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);

		static void FwtInternal(long[] c)
		{
			var n = c.Length;
			for (int i = 1; i < n; i <<= 1)
				for (int j = 0; j < n; j++)
				{
					if ((j & i) != 0) continue;
					var x = c[j];
					var y = c[j | i];
					c[j] = MInt(x + y);
					c[j | i] = MInt(x - y);
				}
		}

		// n: Power of 2
		// XOR
		public static long[] Fwt(long[] c, bool inverse = false)
		{
			var n = c.Length;
			var nInv = MInv(n);
			var r = new long[n];
			c.CopyTo(r, 0);
			FwtInternal(r);
			if (inverse) for (int i = 0; i < n; ++i) r[i] = r[i] * nInv % M;
			return r;
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			var fa = Fwt(a);
			var fb = Fwt(b);
			for (int i = 0; i < a.Length; ++i) fa[i] = fa[i] * fb[i] % M;
			return Fwt(fa, true);
		}
	}
}
