using System;

namespace CoderLib6.Values
{
	// M が素数に限定されるメソッド: MInv, MNcr
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/7/DPL/5
	static class ModuloHelper
	{
		//const long M = 998244353;
		const long M = 1000000007;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
		static long MInv(long x) => MPow(x, M - 2);

		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static long MNeg(long x) => MInt(-x);
		static long MAdd(long x, long y) => MInt(x + y);
		static long MSub(long x, long y) => MInt(x - y);
		static long MMul(long x, long y) => MInt(x * y);
		static long MDiv(long x, long y) => MInt(x * MInv(y));

		static long[] MPows(long b, int n)
		{
			var p = new long[n + 1];
			p[0] = 1;
			for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
			return p;
		}

		// n >= 0
		static long MFactorial(int n) { for (long x = 1, i = 1; ; x = x * ++i % M) if (i >= n) return x; }
		static long MNpr(int n, int r)
		{
			if (n < r) return 0;
			for (long x = 1, i = n - r; ; x = x * ++i % M) if (i >= n) return x;
		}
		static long MNcr(int n, int r) => n < r ? 0 : n - r < r ? MNcr(n, n - r) : MNpr(n, r) * MInv(MFactorial(r)) % M;
	}
}
